using DomainLayer.Models.AuthorModule;
using DomainLayer.Models.BookModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBookAuthorRepository
    {
        // Assign multiple authors to a book
        Task AssignAuthorsToBookAsync(int bookId , List<int> authorsId);

        // Assign multiple books to an author
        Task AssignBooksToAuthorAsync(int authorId, List<int> booksId);

        // Get all authors of a book
        Task<IEnumerable<Author>> GetAllAuthorsByBookIdAsync(int bookId);

        // Get all books of an author
        Task<IEnumerable<Book>> GetAllBooksByAuthorIdAsync(int authorId);

    }
}
