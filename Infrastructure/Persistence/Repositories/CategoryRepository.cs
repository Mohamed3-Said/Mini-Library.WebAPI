using DomainLayer.Contracts;
using DomainLayer.Models.CategoryModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CategoryRepository(LibraryDbContext _dbContext) : ICategoryRepository
    {
        public async Task AddAsync(Category category) => await _dbContext.Categories.AddAsync(category);
        public async Task UpdateAsync(Category category) => _dbContext.Categories.Update(category);

        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
                _dbContext.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Include(c=>c.Books)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
           return await _dbContext.Categories
                .AsNoTracking()
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
