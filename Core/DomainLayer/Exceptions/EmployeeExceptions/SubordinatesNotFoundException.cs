using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.EmployeeExceptions
{
    public sealed class SubordinatesNotFoundException(int supervisorId) : NotfoundException($"No subordinates found for Supervisor id {supervisorId}")
    {
    }
}
