using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.BookAuthorModuleDto
{
    public class AssignAuthorsToBookDto
    {
        public int BookId { get; set; }
        public List<int> AuthorsId { get; set; } = new List<int>();
    }
}
