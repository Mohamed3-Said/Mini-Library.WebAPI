using DomainLayer.Models.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string SSN);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetUserBySSNAsync(string SSN);
        Task<IEnumerable<User>> GetUsersByEmployeeAsync(int employeeId);
    }
}
