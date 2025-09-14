using DomainLayer.Models.AuthorModule;
using DomainLayer.Models.CategoryModule;
using DomainLayer.Models.PublisherModule;
using DomainLayer.Models.ShelfModule;
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

        // Publisher relation
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; } = default!;

        // Category relation
        public int? CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        // Shelf relation
        public string? ShelfCode { get; set; } 
        public Shelf Shelf { get; set; } = default!;
    }
}

