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

        // Constructor
        public UnitOfWork(LibraryDbContext dbContext, IUserBorrowRepository userBorrowRepository)
        {
            _dbContext = dbContext;
            UserBorrowRepository = userBorrowRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

