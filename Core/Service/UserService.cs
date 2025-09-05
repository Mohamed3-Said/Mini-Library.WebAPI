using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.UserExceptions;
using DomainLayer.Models.UserModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.UserModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService(IUserRepository _userRepository, IMapper _mapper) : IUserService
    {
        public async Task<UserToReadDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<CreateUserDto, User>(createUserDto);
            await _userRepository.AddAsync(user);
            return _mapper.Map<User, UserToReadDto>(user);
        }

        public async Task<UserToReadDto> UpdateUserAsync(string ssn, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserBySSNAsync(ssn);
            if (user is not null)
            {
                _mapper.Map<UpdateUserDto, User>(updateUserDto,user);
                await _userRepository.UpdateAsync(user);
                return _mapper.Map<User, UserToReadDto>(user);
            }
            throw new UserNotFoundException(ssn);
        }

        public async Task<bool> DeleteUserAsync(string ssn)
        {
            var user = await _userRepository.GetUserBySSNAsync(ssn);
            if (user is null) return false;
            else
            {
                await _userRepository.DeleteAsync(ssn);
                return true;
            }
        }

        public async Task<IEnumerable<UserToReadDto>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<UserToReadDto>>(users);
            return usersDto;
        }

        public async Task<UserToReadDto> GetUserBySSNAsync(string ssn)
        {
            var user = await _userRepository.GetUserBySSNAsync(ssn);
            if (user is not null)
            {
                var UserDto = _mapper.Map<User, UserToReadDto>(user);
                return UserDto;
            }
            throw new UserNotFoundException(ssn);
        }

        public async Task<IEnumerable<UserToReadDto>> GetUsersByEmployeeId(int employeeId)
        {
            var Users = await _userRepository.GetUsersByEmployeeAsync(employeeId);
            var UsersDto = _mapper.Map<IEnumerable<User>,IEnumerable<UserToReadDto>>(Users);
            return UsersDto;

        }

    }
}
