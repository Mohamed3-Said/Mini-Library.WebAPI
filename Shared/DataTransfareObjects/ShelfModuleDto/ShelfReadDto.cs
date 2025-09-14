using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.ShelfModuleDto
{
    public class ShelfReadDto
    {
        public string Code { get; set; } = default!;
        public ICollection<string>? Books { get; set; }
    }
}
