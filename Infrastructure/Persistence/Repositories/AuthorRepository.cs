using DomainLayer.Contracts;
using DomainLayer.Models.AuthorModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AuthorRepository(LibraryDbContext _dbContext) : IAuthorRepository
    {
        public async Task AddAsync(Author author) => await _dbContext.Authors.AddAsync(author);

        public async Task UpdateAsync(Author author) =>   _dbContext.Authors.Update(author);
        public async Task DeleteAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if(author is not null)
            {
                _dbContext.Authors.Remove(author);
            }
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
           return await _dbContext.Authors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
           return await _dbContext.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(a=>a.AuthorId == id);
        }

    }
}
