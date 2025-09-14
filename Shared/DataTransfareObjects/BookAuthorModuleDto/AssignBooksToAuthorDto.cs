using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.BookAuthorModuleDto
{
    public class AssignBooksToAuthorDto
    {
        public int AuthorId { get; set; }
        public List<int> BooksId { get; set; } = new List<int>();
    }
}
