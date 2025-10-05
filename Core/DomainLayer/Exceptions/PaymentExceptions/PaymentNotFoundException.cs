using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.PaymentExceptions
{
    public sealed class PaymentNotFoundException(int paymentId) : NotfoundException($"Payment With Id {paymentId} is Not Found ")
    {
    }
}
