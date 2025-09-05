using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.UserExceptions
{
    public class UserNotFoundException(string ssn) : NotfoundException($"User With SSN {ssn} is Not Found!!")
    {
    }
}
