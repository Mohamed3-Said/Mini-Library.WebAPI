using Shared.DataTransfareObjects.EmployeeModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IEmployeeService
    {
       Task<IEnumerable<EmployeeReadDto>> GetAllAsync();
       Task<EmployeeReadDto?> GetByIdAsync(int id);
       Task<EmployeeReadDto> AddAsync(CreateEmployeeDto createEmployee);
       Task<int> Update(int id, UpdateEmployeeDto updateEmployee);
       Task<bool> Delete(int id);
       Task<IEnumerable<EmployeeReadDto>> GetSubordinatesAsync(int supervisorId);
    }
}
