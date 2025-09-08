using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.BookExceptions
{
    public sealed class UserBorrowNotFoundException(int UBorrowId) : NotfoundException($"UserBorrow With id {UBorrowId} is Not Found")
    {
    }
}
