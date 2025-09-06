using DomainLayer.Contracts;
using DomainLayer.Models.BookModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BookRepository(LibraryDbContext _dbContext) : IBookRepository
    {
        public async Task AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
             _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                _dbContext.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
          return await _dbContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
        }

    }
}
