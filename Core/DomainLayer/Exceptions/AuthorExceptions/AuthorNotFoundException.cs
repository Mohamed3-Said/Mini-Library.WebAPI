using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.AuthorExceptions
{
    public sealed class AuthorNotFoundException(int id) : NotfoundException($"Author With id {id} is Not found")
    {
    }
}
