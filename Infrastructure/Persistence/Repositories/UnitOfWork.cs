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

        // Constructor
        public UnitOfWork(LibraryDbContext dbContext, IUserBorrowRepository userBorrowRepository, IAuthorRepository authorRepository)
        {
            _dbContext = dbContext;
            UserBorrowRepository = userBorrowRepository;
            AuthorRepository = authorRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

