using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.EmployeeExceptions
{
    public class EmpoyeeBadRequestException(List<string> errors) : Exception("Invalid Employee Request")
    {
        public List<string> Errors { get; } = errors;
    }
}
