using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ServiceAbstraction;
using Shared.DataTransfareObjects.AuthorModuleDto;
using Shared.DataTransfareObjects.UserBorrowModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        //1- Creat Author EndPoint => Post : BaseUrl\api\Author\Create
        [HttpPost("Create")]
        public async Task<ActionResult<AuthorToReadDto>> CreateAuthor(CreateAuthorDto authorDto)
        {
            var author = await _authorService.CreateAuthorAsync(authorDto);
            return Ok(author);
        }

        //2- Update Author EndPoint => Put : BaseUrl\api\Author\Update
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorToReadDto>> UpdateAuthor(int id , UpdateAuthorDto authorDto)
        {
            var author = await _authorService.UpdateAuthorAsync(id,authorDto);
            return Ok(author);
        }


        //3- Delete Author EndPoint => Delete : BaseUrl\api\Author\Id
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteAuthor(int Id)
        {
            var author = await _authorService.DeleteAuthorAsync(Id);
            return Ok(author);
        }


        //4- Get All Author EndPoint => Get : BaseUrl\api\Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorToReadDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }


        //5- Get Author By Id EndPoint => Get : BaseUrl\api\Author\Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<AuthorToReadDto>> GetAuthorById(int Id)
        {
            var authors = await _authorService.GetAuthorByIdAsync(Id);
            return Ok(authors);
        }

    }
}
