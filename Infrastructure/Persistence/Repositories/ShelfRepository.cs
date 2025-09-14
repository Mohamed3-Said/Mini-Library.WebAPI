using DomainLayer.Contracts;
using DomainLayer.Models.ShelfModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ShelfRepository(LibraryDbContext _dbContext) : IShelfRepository
    {
        public async Task AddAsync(Shelf shelf) => await _dbContext.Shelves.AddAsync(shelf);

        public async Task DeleteAsync(string Code)
        {
            var shelf = await _dbContext.Shelves.FindAsync(Code);
            if (shelf != null)
                _dbContext.Shelves.Remove(shelf);
        }
        public async Task UpdateAsync(Shelf shelf) => _dbContext.Shelves.Update(shelf);

        public async Task<IEnumerable<Shelf>> GetAllAsync()
        {
            return await _dbContext.Shelves
                .AsNoTracking()
                .Include(s => s.Books)
                .ToListAsync();
        }

        public Task<Shelf?> GetByCodeAsync(string Code)
        {
            return _dbContext.Shelves
                 .AsNoTracking()
                 .Include(s => s.Books)
                 .FirstOrDefaultAsync(s => s.Code == Code);
        }

    }
}
