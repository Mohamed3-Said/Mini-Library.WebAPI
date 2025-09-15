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
    public class UserBorrowRepository(LibraryDbContext _dbContext) : IUserBorrowRepository
    {
        public async Task AddAsync(UserBorrow userBorrow) => await _dbContext.UserBorrows.AddAsync(userBorrow);
        public async Task UpdateAsync(UserBorrow userBorrow) => _dbContext.UserBorrows.Update(userBorrow);

        public async Task DeleteAsync(int UserBorrowId)
        {
            var userborrow = await _dbContext.UserBorrows.FindAsync(UserBorrowId);
            if (userborrow != null)
                _dbContext.UserBorrows.Remove(userborrow);
        }

        public async Task<IEnumerable<UserBorrow>> GetAllAsync()
        {
           return await _dbContext.UserBorrows
                .AsNoTracking()
                .Include(ub=>ub.User)
                .Include(ub=>ub.Book)
                .Include(ub=>ub.Employee)
                .ToListAsync();
        }
        public async Task<UserBorrow?> GetByIdAsync(int UserBorrowId)
        {
          return await _dbContext.UserBorrows
                .AsNoTracking()
                .Include(ub => ub.User)
                .Include(ub => ub.Book)
                .Include(ub => ub.Employee)
                .FirstOrDefaultAsync(UB => UB.UserBorrowId == UserBorrowId);
        }
        public async Task<IEnumerable<UserBorrow>> GetBorrowsByBookId(int BookId)
        {
            return await _dbContext.UserBorrows
                 .AsNoTracking()
                 .Include(ub => ub.User)
                 .Include(ub => ub.Book)
                 .Include(ub => ub.Employee)
                 .Where(UB=>UB.BookId == BookId)  
                 .ToListAsync();
                
        }

        public async Task<IEnumerable<UserBorrow>> GetBorrowsByUserSSN(string ssn)
        {
           return await _dbContext.UserBorrows
                .AsNoTracking()
                .Include(ub => ub.User)
                .Include(ub => ub.Book)
                .Include(ub => ub.Employee)
                .Where(UB=>UB.UserSSN == ssn)
                .ToListAsync();
        }


    }
}
