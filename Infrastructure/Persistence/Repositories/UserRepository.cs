using DomainLayer.Contracts;
using DomainLayer.Models.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository(LibraryDbContext _dbContext) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
          await  _dbContext.Users.AddAsync(user);
          await _dbContext.SaveChangesAsync();         
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string SSN)
        {
            //check of Id:
            var user = await _dbContext.Users.FindAsync(SSN);
            if (user is not null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
          return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetUserBySSNAsync(string SSN)
        {
            return await _dbContext.Users
                .AsNoTracking().
                FirstOrDefaultAsync(U => U.SSN == SSN);
        }

        public async Task<IEnumerable<User>> GetUsersByEmployeeAsync(int employeeId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(U=>U.EmployeeId==employeeId)
                .ToListAsync();
        }

    }
}
