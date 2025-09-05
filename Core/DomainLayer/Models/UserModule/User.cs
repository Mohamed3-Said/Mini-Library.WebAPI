using DomainLayer.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.UserModule
{
    public class User
    {
        public string SSN { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;

        //Relationship Employee:
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
