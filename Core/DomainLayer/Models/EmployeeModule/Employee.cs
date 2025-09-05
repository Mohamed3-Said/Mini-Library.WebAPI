using DomainLayer.Models.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.EmployeeModule
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FName { get; set; } =default!;
        public string LName { get; set; } =default!;
        public string Email { get; set; } =default!;
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Bonus { get; set; }
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        //Relationship Supervisor:
        public  int? SupervisorId { get; set; }
        public Employee? Supervisor { get; set; } //Manager

        //Relationship Subordinates: One to Many Self-Referencing
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
        //Relationship USers
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
