using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.CategorModuleExceptions
{
    public class CategoryNotFoundException(int id) : NotfoundException($"Category With id {id} is Not Found")
    {

    }
}
