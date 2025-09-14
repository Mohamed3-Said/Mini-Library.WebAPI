using DomainLayer.Contracts;
using DomainLayer.Models.AuthorModule;
using DomainLayer.Models.BookModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BookAuthorRepository(LibraryDbContext _dbContext) : IBookAuthorRepository
    {
        public async Task AssignAuthorsToBookAsync(int bookId, List<int> authorsId)
        {
            foreach (var authorId in authorsId)
            {
                if (!await _dbContext.BookAuthors.AnyAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId))
                {
                    // Relation Not found => Add Reltion:
                    _dbContext.BookAuthors.Add(new BookAuthor
                    {
                        AuthorId = authorId,
                        BookId = bookId
                    });
                }
            }
        }

        public async Task AssignBooksToAuthorAsync(int authorId, List<int> booksId)
        {
            foreach (var bookId in booksId)
            {
                if (!await _dbContext.BookAuthors.AnyAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId))
                {
                    // Relation Not found => Add Relation :
                    _dbContext.BookAuthors.Add(new BookAuthor
                    {
                        BookId = bookId,
                        AuthorId = authorId
                    });
                }
            }
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsByBookIdAsync(int bookId)
        {
            return await _dbContext.BookAuthors
                 .Where(ba => ba.BookId == bookId)
                 .Select(ba => ba.Author)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksByAuthorIdAsync(int authorId)
        {
            return await _dbContext.BookAuthors
                 .Where(ba => ba.AuthorId == authorId)
                 .Select(ba => ba.Book)
                 .ToListAsync();
        }
    }
}
