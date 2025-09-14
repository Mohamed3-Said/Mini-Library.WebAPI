using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        IUserBorrowRepository UserBorrowRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookAuthorRepository BookAuthorRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IShelfRepository ShelfRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
