using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BadRequestException : Exception
    {
        public List<string> Errors { get; }

        // Constructor يقبل رسالة واحدة
        public BadRequestException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        // Constructor يقبل لستة رسائل
        public BadRequestException(List<string> errors) : base("Invalidation Failed")
        {
            Errors = errors;
        }
    }

}
