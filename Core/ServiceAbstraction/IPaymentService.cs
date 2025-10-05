using Shared.DataTransfareObjects.PaymentModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {

        Task<PaymentToReadDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);

        Task<PaymentToReadDto?> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto);

        Task<bool> DeletePaymentAsync(int paymentId);

        Task<IEnumerable<PaymentToReadDto>> GetAllPaymentsAsync();

        Task<PaymentToReadDto?> GetPaymentByIdAsync(int paymentId);

        Task<IEnumerable<PaymentToReadDto>> GetPaymentsByUserSSNAsync(string userSSN);
    }
}
