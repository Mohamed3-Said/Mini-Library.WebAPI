using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.UserBorrowModuleDto
{
    public class UserBorrowToReadDto
    {
        public int UserBorrowId { get; set; }
        public string UserSSN { get; set; } = default!;
        public string UserName { get; set; } = default!;  // Optionally include user name
        public int BookId { get; set; }
        public string BookTitle { get; set; } = default!; // Optionally include book title
        public DateTime Date_Borrowed { get; set; }
        public DateTime Due_Date { get; set; }
        public decimal AmountOfMoney { get; set; }
    }
}
