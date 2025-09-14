using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.PublisherModuleDto
{
    public class PublisherToReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<string>? Books { get; set; }
    }
}
