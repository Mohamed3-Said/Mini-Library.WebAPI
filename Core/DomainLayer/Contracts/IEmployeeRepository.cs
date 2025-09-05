using DomainLayer.Models.EmployeeModule;
using DomainLayer.Models.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);

        //Custom Method :
        Task<IEnumerable<Employee>> GetSubordinatesAsync(int supervisorId);


    }
}
