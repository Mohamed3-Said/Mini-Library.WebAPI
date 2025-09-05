using DomainLayer.Models.AuthorModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BookModule
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;

        // Relationships
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}

