using DomainLayer.Contracts;
using DomainLayer.Models.PublisherModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PublisherRepository(LibraryDbContext _dbContext) : IPublisherRepository
    {
        public async Task AddAsync(Publisher publisher) => await _dbContext.Publishers.AddAsync(publisher);
        public async Task UpdateAsync(Publisher publisher) => _dbContext.Publishers.Update(publisher);
        public async Task DeleteAsync(int id)
        {
            var publisher =  await _dbContext.Publishers.FindAsync(id);
            if(publisher != null)
                _dbContext.Publishers.Remove(publisher);
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
          return await _dbContext.Publishers
                .AsNoTracking()
                .Include(p=>p.Books)
                .ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
           return await _dbContext.Publishers
                .AsNoTracking()
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Id == id);  
        }

    }
}
