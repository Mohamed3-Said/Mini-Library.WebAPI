using DomainLayer.Models.BookModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.ShelfModule
{
    public class Shelf
    {
        public string Code { get; set; } = default!;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
