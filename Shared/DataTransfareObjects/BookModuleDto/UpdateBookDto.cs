using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.BookModuleDto
{
    public class UpdateBookDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = default!;
    }
}
