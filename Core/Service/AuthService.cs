using DomainLayer.Exceptions;
using DomainLayer.Exceptions.IdentityExceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransfareObjects.AuthorModuleDto;
using Shared.DataTransfareObjects.IdentityModuleDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService(UserManager<AppUser> _userManager, IConfiguration _configuration , IEmailService _emailService) : IAuthService
    {

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            //1-check Email is exist :
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                throw new UserNotfoundException(loginDto.Email);
            //2-check password is correct :
            var PasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            var roles = await _userManager.GetRolesAsync(user);
            if (PasswordValid)
            {
                return new AuthResponseDto { DisplayName = user.DisplayName, Role = roles.FirstOrDefault() ?? "User", Email = user.Email!, Token = await CreateTokenAsync(user) };
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
            if (Result.Succeeded)
            {
                //add role to user :
                await _userManager.AddToRoleAsync(user, registerDto.Role);
                return new AuthResponseDto { DisplayName = user.DisplayName, Email = user.Email, Role = registerDto.Role, Token = await CreateTokenAsync(user) };
            }
            else
            {
                //errors from Result and Add to exception:
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }



        public async Task ForgotPasswordAsync(string email)
        {
            //check if email exist :
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return; //to prevent email enumeration
            }
            //Generate password reset token :
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //Encode token to be URL safe :
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            //Create reset link (you can change the URL to your frontend reset password page) :
            var resetBase = _configuration.GetSection("ClientApp")["ResetPasswordUrl"];
            var resetLink = $"{resetBase}?email={WebUtility.UrlEncode(user.Email)}&token={encodedToken}";

            //Send email with reset link (you need to implement your email service) :
            var htmlBody = $@"
                <p>Hello {user.DisplayName ?? user.UserName},</p>
                <p>To reset your password, click the link below:</p>
                <p><a href='{resetLink}'>Reset your password</a></p>
                <p>If you didn't request this, ignore this email.</p>
            ";
            //Send Email From the MailKitEmailService :
            await _emailService.SendEmailAsync(user.Email!, "Reset your password", htmlBody);
        }
        public async Task ResetPasswordAsync(ResetPasswordDto model)
        {
           //1-check of valid model in newpassword :
           if(model.NewPassword != model.ConfirmPassword )
                throw new BadRequestException("New password and confirm password do not match.");
            //2-check if email exist :
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new BadRequestException("Invalid Request");
            //3-Decode the token :
            string decodedToken;
            try
            {
                var tokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                decodedToken = Encoding.UTF8.GetString(tokenBytes);

            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid token.");
            }

            //4-Reset the password From userManager : // استدعاء ResetPasswordAsync من UserManager مع التوكن المفكوك وكلمة السر الجديدة
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
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
            foreach (var role in Roles)
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
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials,
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"]
            );
            //7-Return token :
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
