using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.IdentityModuleDto
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress]
        public string Email { get; set; } = default!;
    }
}
