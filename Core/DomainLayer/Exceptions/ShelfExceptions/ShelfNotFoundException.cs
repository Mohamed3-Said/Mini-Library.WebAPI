using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.ShelfExceptions
{
    public sealed class ShelfNotFoundException(string code) : NotfoundException($"Shelf With Code {code} is Not Found")
    {
    }
}
