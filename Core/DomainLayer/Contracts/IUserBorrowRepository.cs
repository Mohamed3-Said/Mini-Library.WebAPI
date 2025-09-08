using DomainLayer.Models.BookModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUserBorrowRepository
    {
        Task AddAsync(UserBorrow userBorrow);
        Task UpdateAsync(UserBorrow userBorrow);
        Task DeleteAsync(int UserBorrowId);
        Task<IEnumerable<UserBorrow>> GetAllAsync();
        Task<UserBorrow?> GetByIdAsync(int UserBorrowId);
        Task<IEnumerable<UserBorrow>> GetBorrowsByUserSSN(string ssn);
        Task<IEnumerable<UserBorrow>> GetBorrowsByBookId(int BookId);
    }
}
