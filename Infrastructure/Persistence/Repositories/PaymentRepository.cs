using DomainLayer.Contracts;
using DomainLayer.Models.PaymentModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PaymentRepository(LibraryDbContext _dbContext) : IPaymentRepository
    {
        public async Task AddAsync(Payment payment) => await _dbContext.payments.AddAsync(payment);
        public async Task UpdateAsync(Payment payment) =>  _dbContext.payments.Update(payment);
        public async Task DeleteAsync(int paymentId)
        {
           var payment = await _dbContext.payments.FindAsync(paymentId);
            if (payment != null)
            {
                _dbContext.payments.Remove(payment);
            }

        }
        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
           return await _dbContext.payments.AsNoTracking().ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
           return await _dbContext.payments.AsNoTracking().FirstOrDefaultAsync(p=>p.PaymentId == id);
        }

        public async Task<IEnumerable<Payment>> GetByUserSSNAsync(string userSSN)
        {
            return await _dbContext.payments
                .AsNoTracking()
                .Where(p=>p.UserSSN == userSSN)
                .ToListAsync();
        }

    }
}
