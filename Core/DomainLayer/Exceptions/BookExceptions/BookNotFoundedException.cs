using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.BookExceptions
{
    public class BookNotFoundedException(int id ) : NotfoundException($"Book With id {id} is Not Found ")
    {

    }
}
