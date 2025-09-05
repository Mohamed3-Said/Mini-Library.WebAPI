using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.EmployeeModuleDtos
{
    public class EmployeeReadDto
    {
        public int EmpId { get; set; }
        public string FName { get; set; } = default!;
        public string LName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public decimal Salary { get; set; } 
        public decimal Bonus { get; set; }
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public int? SupervisorId { get; set; }
        public List<EmployeeReadDto> Subordinates { get; set; } = new (); // للـSubordinates
    }
}
