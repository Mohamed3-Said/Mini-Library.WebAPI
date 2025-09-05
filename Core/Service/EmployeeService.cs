using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.EmployeeExceptions;
using DomainLayer.Models.EmployeeModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.EmployeeModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeService
    {
        public async Task<EmployeeReadDto> AddAsync(CreateEmployeeDto createEmployeedto)
        {

           var employee = _mapper.Map<CreateEmployeeDto,Employee>(createEmployeedto);
            await _employeeRepository.AddAsync(employee);
           return _mapper.Map<Employee,EmployeeReadDto>(employee);
        }

        public async Task<int> Update(int id, UpdateEmployeeDto updateEmployeedto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if(employee is not null)
            {
                _mapper.Map<UpdateEmployeeDto, Employee>(updateEmployeedto, employee);
                await _employeeRepository.UpdateAsync(employee);
                return employee.EmpId;

            }
            throw new EmployeeNotFoundException(id);
        }
        public async Task<bool> Delete(int id)
        {
           var employee = await _employeeRepository.GetByIdAsync(id);
            if
                (employee is null) return false;
            else
            {
               await _employeeRepository.DeleteAsync(id);
                return true;
            }
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeReadDto>>(employees);
            return employeeDtos;
        }

        public async Task<EmployeeReadDto?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee is null)
                throw new EmployeeNotFoundException(id);
            var employeeDto = _mapper.Map<Employee, EmployeeReadDto>(employee);
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetSubordinatesAsync(int supervisorId)
        {
            var suberdinates = await _employeeRepository.GetSubordinatesAsync(supervisorId);
            if (!suberdinates.Any())
                throw new SubordinatesNotFoundException(supervisorId);
            var suberdinatesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeReadDto>>(suberdinates);
            return suberdinatesDto;

        }

    }
}
