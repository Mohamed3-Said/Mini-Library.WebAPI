using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ServiceAbstraction;
using Shared.DataTransfareObjects.UserBorrowModuleDto;
using Shared.DataTransfareObjects.UserModuleDto;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class UserBorrowController : ControllerBase
    {
        private readonly IUserBorrowService _userBorrowService;

        public UserBorrowController(IUserBorrowService userBorrowService)
        {
            _userBorrowService = userBorrowService;
        }
        //1- Creat UserBorrow EndPoint => Post : BaseUrl\api\UserBorrow\Create
        [HttpPost("Create")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<UserBorrowToReadDto>> CreateUserBorrow(CreateUserBorrowDto createborrowDto)
        {
            var userborrow = await _userBorrowService.CreateBorrowAsync(createborrowDto);
            return Ok(userborrow);
        }

        //2- Update UserBorrow EndPoint => Put : BaseUrl\api\UserBorrow\Update
        [HttpPut("Update")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<UserBorrowToReadDto>> UpdateUserBorrow(UpdateUserBorrowDto updateborrowDto)
        {
            var userborrow = await _userBorrowService.UpdateBorrowAsync(updateborrowDto);
            return Ok(userborrow);
        }

        //3- Delete UserBorrow EndPoint => Delete : BaseUrl\api\UserBorrow\Id
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<bool>> DeleteUserBorrow(int Id)
        {
            var userborrow = await _userBorrowService.DeleteBorrowAsync(Id);
            return Ok(userborrow);
        }
        //4- Get All UserBorrow EndPoint => Get : BaseUrl\api\UserBorrow
        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<IEnumerable<UserBorrowToReadDto>>> GetAllUserBorrow()
        {
            var userborrows = await _userBorrowService.GetAllBorrowsAsync();
            return Ok(userborrows);
        }

        //5- Get UserBorrow By Id EndPoint => Get : BaseUrl\api\UserBorrow\Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserBorrowToReadDto>> GetUserBorrowById(int Id)
        {
            var userborrow = await _userBorrowService.GetBorrowByIdAsync(Id);
            return Ok(userborrow);
        }

        //5- Get UserBorrow By BookId EndPoint => Get : BaseUrl\api\UserBorrow\ByBookId\Id
        [HttpGet("ByBookId/{Id}")]
        public async Task<ActionResult<IEnumerable<UserBorrowToReadDto>>> GetUserBorrowByBookId(int Id)
        {
            var userborrow = await _userBorrowService.GetBorrowsByBookIdAsync(Id);
            return Ok(userborrow);
        }

        //6- Get UserBorrow By UserSSN EndPoint => Get : BaseUrl\api\UserBorrow\ByUserSSN\SSN
        [HttpGet("ByUserSSN/{SSN}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<IEnumerable<UserBorrowToReadDto>>> GetUserBorrowByUserSSN(string SSN)
        {
            var userborrow = await _userBorrowService.GetBorrowsByUserSSNAsync(SSN);
            return Ok(userborrow);
        }


    }
}
