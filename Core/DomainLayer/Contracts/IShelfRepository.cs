using DomainLayer.Models.CategoryModule;
using DomainLayer.Models.ShelfModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IShelfRepository
    {
        Task<IEnumerable<Shelf>> GetAllAsync();
        Task<Shelf?> GetByCodeAsync(string Code);
        Task AddAsync(Shelf  shelf);
        Task UpdateAsync(Shelf shelf);
        Task DeleteAsync(string Code);
    }
}
