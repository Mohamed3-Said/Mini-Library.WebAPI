using DomainLayer.Models.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BookModule
{
    public class UserBorrow
    {
        public int UserBorrowId { get; set; } // Primary Key
        public string UserSSN { get; set; } = string.Empty;
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime Date_Borrowed { get; set; }
        public DateTime Due_Date { get; set; }
        public decimal AmountOfMoney { get; set; }
    }
}
