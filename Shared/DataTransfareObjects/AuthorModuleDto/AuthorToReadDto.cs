using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.AuthorModuleDto
{
    public class AuthorToReadDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = default!;
    }
}
