using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.PublisherExceptions
{
    public sealed class PublisherNotFoundException(int id) : NotfoundException($"Publisher With id {id} is Not Found")
    {

    }
}
