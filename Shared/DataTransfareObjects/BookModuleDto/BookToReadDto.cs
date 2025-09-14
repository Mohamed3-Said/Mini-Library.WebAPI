using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.BookModuleDto
{
    public class BookToReadDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = default!;

        public string PublisherName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string ShelfCode { get; set; } = default!;
    }
}
