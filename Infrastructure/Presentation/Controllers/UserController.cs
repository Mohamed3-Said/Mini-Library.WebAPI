using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.UserModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController(IUserService _userService) : ControllerBase
    {
        //Create User Endpoint => Post : BaseURl\api\User\Create
        [HttpPost("Create")]
        public async Task<ActionResult<UserToReadDto>> CreateUser(CreateUserDto createUserDto)
        {
            var user = await _userService.CreateUserAsync(createUserDto);
            return Ok(user);
        }

        //Update User Endpoint => Put : BaseURl\api\User\{ssn}
        [HttpPut("{ssn}")]
        public async Task<ActionResult<UserToReadDto>> UpdateUser(string ssn, UpdateUserDto updateUserDto)
        {
            var user = await _userService.UpdateUserAsync(ssn, updateUserDto);
            return Ok(user);
        }

        //Delete User Endpoint => Delete : BaseURl\api\User\{ssn}
        [HttpDelete("{ssn}")]
        public async Task<ActionResult<bool>> DeleteUser(string ssn)
        {
            var user = await _userService.DeleteUserAsync(ssn);
            return Ok(user);
        }

        //Get All User Endpoint => Get : BaseURl\api\User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserToReadDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }

        //Get User By SSN Endpoint => Get : BaseURl\api\User\{ssn}
        [HttpGet("{ssn}")]
        public async Task<ActionResult<UserToReadDto>> GetUserById(string ssn)
        {
            var user = await _userService.GetUserBySSNAsync(ssn);
            return Ok(user);
        }

        //Get All User By Employee Id Endpoint => Get : BaseURl\api\User\{EmpId}
        [HttpGet("ByEmployee/{Id}")]
        public async Task<ActionResult<IEnumerable<UserToReadDto>>> GetUserByEmployeeId(int Id)
        {
            var users = await _userService.GetUsersByEmployeeId(Id);
            return Ok(users);

        }
    }
}
