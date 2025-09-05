using DomainLayer.Contracts;
using DomainLayer.Models.EmployeeModule;
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
    public class EmployeeRepository(LibraryDbContext _dbContext) : IEmployeeRepository
    {
        public async Task AddAsync(Employee employee)
        {
           await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public  async Task UpdateAsync(Employee employee)
        {
             _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
              await  _dbContext.SaveChangesAsync();
            }
        }
      
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
           return  await _dbContext.Employees.AsNoTracking().Include(e=>e.Subordinates).ToListAsync();  
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
           return await _dbContext.Employees
                .AsNoTracking()
                .Include(e=>e.Subordinates)
                .Include(e=>e.Users)
                .FirstOrDefaultAsync(e=>e.EmpId==id);

        }

        public async Task<IEnumerable<Employee>> GetSubordinatesAsync(int supervisorId)
        {
          return  await _dbContext.Employees.AsNoTracking()
                .Where(e => e.SupervisorId == supervisorId)
                .ToListAsync();
        }

    }
}
