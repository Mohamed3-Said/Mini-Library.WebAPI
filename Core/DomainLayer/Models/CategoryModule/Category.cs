using DomainLayer.Models.BookModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.CategoryModule
{
    public class Category
    {
        public int Id { get; set; }
        public string Cat_Name { get; set; } = default!;
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
