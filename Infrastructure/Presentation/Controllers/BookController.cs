using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookController(IBookService _bookService) : ControllerBase
    {
        //Create Book EndPoint => Post : BaseUrl\api\Book\Create
        [HttpPost("Create")]
        public async Task<ActionResult<BookToReadDto>> CreateBook(CreateBookDto bookDto)
        {
            var Book = await _bookService.CreateBookAsync(bookDto);
            return Ok(Book);
        }

        //Update Book EndPoint => Put : BaseUrl\api\Book\id
        [HttpPut("{id}")]
        public async Task<ActionResult<BookToReadDto>> UpdateBook(int id , UpdateBookDto bookDto)
        {
            var Book = await _bookService.UpdateBookAsync( id , bookDto);
            return Ok(Book);
        }

        //Delete Book EndPoint => Delete : BaseUrl\api\Book\id
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            var Book = await _bookService.DeleteBookAsync(id);
            return Ok(Book);
        }

        //Get All Books EndPoint => GET : BaseUrl\api\Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookToReadDto>>> GetAllBooks()
        {
            var Books = await _bookService.GetAllBooksAsync();
            return Ok(Books);
        }

        //Get Book By Id EndPoint => GET : BaseUrl\api\Book\id
        [HttpGet("{id}")]
        public async Task<ActionResult<BookToReadDto>> GetBookById(int id)
        {
            var Book = await _bookService.GetBookByIdAsync(id);
            return Ok(Book);
        }

    }
}
