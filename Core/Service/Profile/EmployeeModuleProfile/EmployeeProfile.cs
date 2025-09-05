using DomainLayer.Models.EmployeeModule;
using Shared.DataTransfareObjects.EmployeeModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.EmployeeModuleProfile
{
    public class EmployeeProfile : AutoMapper.Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeReadDto>();
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
           // CreateMap<IEnumerable<Employee>, IEnumerable<EmployeeReadDto>>().ReverseMap();
        }
    }
}
