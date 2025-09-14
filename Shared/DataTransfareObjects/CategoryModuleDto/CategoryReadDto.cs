using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.CategoryModuleDto
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Cat_Name { get; set; } = default!;
        public ICollection<string>? Books { get; set; }
    }
}
