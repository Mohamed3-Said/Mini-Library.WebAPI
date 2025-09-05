using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.UserModuleDto
{
    public class UserToReadDto
    {
        public string SSN { get; set; } = default!;
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public int EmployeeId { get; set; }
    }
}
