using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.UserBorrowModuleDto
{
    public class UpdateUserBorrowDto
    {
        [Required]
        public int UserBorrowId { get; set; } 

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "SSN must be 14 digits.")]
        public string UserSSN { get; set; } = default!;

        [Required]
        public int BookId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime Date_Borrowed { get; set; }

        [Required]
        public DateTime Due_Date { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public decimal AmountOfMoney { get; set; }
    }
}
