using DomainLayer.Models.AuthorModule;
using DomainLayer.Models.BookModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IAuthorRepository
    {

        Task<Author?> GetAuthorByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}
