using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.UserModuleDto
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "SSN must be exactly 14 digits.")]
        public string SSN { get; set; } = default!;

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;

        [Required]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = default!;

        [Required]
        public int EmployeeId { get; set; }
    }
}
