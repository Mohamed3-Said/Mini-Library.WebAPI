using DomainLayer.Models.PaymentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(int paymentId);
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<IEnumerable<Payment>> GetByUserSSNAsync(string userSSN);


    }
}
