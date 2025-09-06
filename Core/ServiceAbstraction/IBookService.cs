using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBookService
    {
        Task<BookToReadDto> GetBookByIdAsync(int id);
        Task<IEnumerable<BookToReadDto>> GetAllBooksAsync();
        Task<BookToReadDto> CreateBookAsync(CreateBookDto bookDto);
        Task<BookToReadDto> UpdateBookAsync(int id , UpdateBookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}
