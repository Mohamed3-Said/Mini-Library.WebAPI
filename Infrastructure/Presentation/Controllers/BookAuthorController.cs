using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.AuthorModuleDto;
using Shared.DataTransfareObjects.BookAuthorModuleDto;
using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;

        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }
        // This controller will handle the relationships between books and authors:

        // 1- Assign multiple authors to a book :
        [HttpPost("AssignAuthorsToBook")]
        public async Task<ActionResult> AssignAuthorsToBook([FromBody]AssignAuthorsToBookDto dto)
        {
            var result = await _bookAuthorService.AssignAuthorsToBookAsync(dto);
            return Ok($"{result} Authors assigned to book successfully.");
        }

        // 2- Assign multiple books to an author
        [HttpPost("AssignBooksToAuthor")]
        public async Task<ActionResult> AssignBooksToAuthor([FromBody]AssignBooksToAuthorDto dto)
        {
            var result = await _bookAuthorService.AssignBooksToAuthorAsync(dto);
            return Ok($"{result} Books assigned to author successfully.");
        }

        // 3- Get all authors of a book:
        [HttpGet("ByBookId/{bookId}")]
        public async Task<ActionResult<IEnumerable<AuthorToReadDto>>> GetAllAuthorsByBookId(int bookId)
        {
            var authors = await _bookAuthorService.GetAllAuthorsByBookIdAsync(bookId);
            return Ok(authors);
        }
        // 4- Get all books of an author : 
        [HttpGet("ByAuthorId/{authorId}")]
        public async Task<ActionResult<IEnumerable<BookToReadDto>>> GetAllBooksByAuthorId(int authorId)
        {
            var authors = await _bookAuthorService.GetAllBooksByAuthorIdAsync(authorId);
            return Ok(authors);
        }

    }
}
