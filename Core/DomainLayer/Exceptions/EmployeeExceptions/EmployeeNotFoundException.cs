using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.EmployeeExceptions
{
    public sealed class EmployeeNotFoundException(int id) : NotfoundException($"Employee With id {id} is Not Found!!")
    {

    }
}
