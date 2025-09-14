using DomainLayer.Models.BookModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.PublisherModule
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        // Navigation property
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
