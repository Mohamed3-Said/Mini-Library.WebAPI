using Shared.DataTransfareObjects.AuthorModuleDto;
using Shared.DataTransfareObjects.BookAuthorModuleDto;
using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBookAuthorService
    {
        // Assign multiple authors to a book
        Task<int> AssignAuthorsToBookAsync(AssignAuthorsToBookDto dto);

        // Assign multiple books to an author
        Task<int> AssignBooksToAuthorAsync(AssignBooksToAuthorDto dto);

        // Get all authors of a book
        Task<IEnumerable<AuthorToReadDto>> GetAllAuthorsByBookIdAsync(int bookId);

        // Get all books of an author
        Task<IEnumerable<BookToReadDto>> GetAllBooksByAuthorIdAsync(int authorId);
    }
}
