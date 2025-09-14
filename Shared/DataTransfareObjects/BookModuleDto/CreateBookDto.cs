using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.BookModuleDto
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        public int PublisherId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShelfCode { get; set; } = default!;
    }
}
