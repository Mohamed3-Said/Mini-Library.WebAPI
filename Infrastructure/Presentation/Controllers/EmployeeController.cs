using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.EmployeeModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController(IEmployeeService _employeeService) : ControllerBase
    {
        //Get all employees Endpoint => Base URL/api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllEmployee()
        {
           var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        //Get employee by id Endpoint => Base URL/api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return Ok(employee);
        }

        //Create new employee Endpoint => Base URL/api/Employee/Create
        [HttpPost("Create")]
        public async Task<ActionResult<EmployeeReadDto>> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = await _employeeService.AddAsync(employeeDto);
            return Ok(employee);
        }
        //Update employee Endpoint => Base URL/api/Employee/Update/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateEmployee(int id , UpdateEmployeeDto employeeDto)
        {
            var employee = await _employeeService.Update(id, employeeDto);
            return Ok(employee);
        }
        //Delete employee Endpoint => Base URL/api/Employee/Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            var isDeleted = await _employeeService.Delete(id);
            return Ok(isDeleted);
        }

        //Get Subordinates Endpoint =>  Baseurl/api/Employee/Subordinates
        [HttpGet("Subordinates")]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetSubordinates(int supervisorId)
        {
            var employeesuperdinates = await _employeeService.GetSubordinatesAsync(supervisorId);
            return Ok(employeesuperdinates);

        }
    }
}
