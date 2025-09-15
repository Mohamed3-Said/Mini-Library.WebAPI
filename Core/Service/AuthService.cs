using DomainLayer.Exceptions;
using DomainLayer.Exceptions.IdentityExceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransfareObjects.AuthorModuleDto;
using Shared.DataTransfareObjects.IdentityModuleDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService(UserManager<AppUser> _userManager , IConfiguration _configuration) : IAuthService
    {
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            //1-check Email is exist :
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user is null)
                throw new UserNotfoundException(loginDto.Email);
            //2-check password is correct :
            var PasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            var roles = await _userManager.GetRolesAsync(user);
            if (PasswordValid)
            {
                return new AuthResponseDto { DisplayName = user.DisplayName, Role = roles.FirstOrDefault() ?? "User" , Email = user.Email!, Token = await CreateTokenAsync(user) };
            }
            else
                throw new UnauthorizedException();
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            //Mapping RegisterDto to AppUser : :
            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            //Create user in database :
            var Result = await _userManager.CreateAsync(user, registerDto.Password);
            if(Result.Succeeded)
            {
                //add role to user :
                await _userManager.AddToRoleAsync(user, registerDto.Role);
                return new AuthResponseDto {DisplayName = user.DisplayName , Email=user.Email, Role = registerDto.Role , Token = await CreateTokenAsync(user) };
            }
            else
            {
                //errors from Result and Add to exception:
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }



        //Create JWT Token :
        private async Task<string> CreateTokenAsync(AppUser user)
        {
            //1-create claims : 
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.NameIdentifier,user.Id!)
            };
            //2-Get user roles :
            var Roles = await _userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //3-Create secrte key :
            var secrtekey = _configuration.GetSection("JWTOptions")["SecretKey"];
            //4-convert secrte key to byte array :
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrtekey!));
            //5-Create signing credentials :
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //6-Create token  :
            var Token = new JwtSecurityToken
            (
                claims : claims,
                expires : DateTime.Now.AddDays(7),
                signingCredentials : credentials,
                issuer : _configuration.GetSection("JWTOptions")["Issuer"],
                audience : _configuration.GetSection("JWTOptions")["Audience"]
            );
            //7-Return token :
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
