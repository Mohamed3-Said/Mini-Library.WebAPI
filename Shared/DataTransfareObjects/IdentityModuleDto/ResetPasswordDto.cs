using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.IdentityModuleDto
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // encoded token from email link

        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not match the Password !!")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
