using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _dbContext;
        public IUserBorrowRepository UserBorrowRepository { get; }

        public IAuthorRepository AuthorRepository {  get; }

        public IBookAuthorRepository BookAuthorRepository { get; }

        public IPublisherRepository PublisherRepository { get; } 

        public ICategoryRepository CategoryRepository { get; }

        public IShelfRepository ShelfRepository { get; }

        public IPaymentRepository PaymentRepository { get; }

        // Constructor
        public UnitOfWork(LibraryDbContext dbContext, IUserBorrowRepository userBorrowRepository,
            IAuthorRepository authorRepository ,
            IBookAuthorRepository bookAuthorRepository , IPublisherRepository publisherRepository,
            ICategoryRepository categoryRepository , IShelfRepository shelfRepository , IPaymentRepository paymentRepository)
        {
            _dbContext = dbContext;
            UserBorrowRepository = userBorrowRepository;
            AuthorRepository = authorRepository;
            BookAuthorRepository = bookAuthorRepository;
            PublisherRepository = publisherRepository;
            CategoryRepository = categoryRepository;
            ShelfRepository = shelfRepository;
            PaymentRepository = paymentRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

