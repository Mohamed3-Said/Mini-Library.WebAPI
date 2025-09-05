using Shared.DataTransfareObjects.UserModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IUserService
    {
        //Get All USers :
        Task<IEnumerable<UserToReadDto>> GetAllUserAsync();
        //Get User By SSN :
        Task<UserToReadDto> GetUserBySSNAsync(string ssn);
        //Add :
        Task<UserToReadDto> CreateUserAsync(CreateUserDto createUserDto);
        //Update :
        Task<UserToReadDto> UpdateUserAsync(string ssn,UpdateUserDto updateUserDto);
        //Delete :
        Task<bool> DeleteUserAsync(string ssn);
        //Get User By EmployeeId :
        Task<IEnumerable<UserToReadDto>> GetUsersByEmployeeId(int employeeId);
    }
}
