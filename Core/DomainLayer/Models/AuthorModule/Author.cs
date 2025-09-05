using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.AuthorModule
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = default!;

        // Relationships between Book and Author (Many-to-Many)
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
