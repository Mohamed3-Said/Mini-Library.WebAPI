using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.IdentityExceptions;
using DomainLayer.Exceptions.PaymentExceptions;
using DomainLayer.Models.PaymentModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.PaymentModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService(IUnitOfWork _unitOfWork, IMapper _mapper ,IUserRepository _userRepository ) : IPaymentService
    {
        public async Task<PaymentToReadDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            //1-Basic Validation:
            if(string.IsNullOrWhiteSpace(createPaymentDto.UserSSN))
                throw new Exception("UserSSN is required.");
            if(createPaymentDto.Amount<=0)
                throw new ArgumentException("Amount must be greater than zero.");

            // 2. Validate user exists
            var user = await _userRepository.GetUserBySSNAsync(createPaymentDto.UserSSN);
            if (user == null)
                throw new UserNotfoundException(createPaymentDto.UserSSN);

            // 3. Map & calculate
            var Payment = _mapper.Map<CreatePaymentDto, Payment>(createPaymentDto);
            // Business Logic Here : Calculate Total Amount , Validate Data , etc ...
            Payment.TotalAmount = Payment.Amount + Payment.DeliveryFee + Payment.GatewayFee;
            await _unitOfWork.PaymentRepository.AddAsync(Payment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Payment, PaymentToReadDto>(Payment);
        }
        public async Task<PaymentToReadDto?> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto)
        {
            var existingPayment = await _unitOfWork.PaymentRepository.GetByIdAsync(updatePaymentDto.PaymentId);
            if (existingPayment == null)
                throw new PaymentNotFoundException(updatePaymentDto.PaymentId);

            if (updatePaymentDto.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            _mapper.Map<UpdatePaymentDto, Payment>(updatePaymentDto, existingPayment);
            // Business Logic Here : Recalculate Total Amount if needed , Validate Data , etc ...
            existingPayment.TotalAmount = existingPayment.Amount + existingPayment.DeliveryFee + existingPayment.GatewayFee;
            await _unitOfWork.PaymentRepository.UpdateAsync(existingPayment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Payment, PaymentToReadDto>(existingPayment);

        }
        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            var existingPayment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);
            if (existingPayment == null)
                throw new PaymentNotFoundException(paymentId);
            await _unitOfWork.PaymentRepository.DeleteAsync(paymentId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PaymentToReadDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentToReadDto>>(payments);
        }

        public async Task<PaymentToReadDto?> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);
            if(payment is null) 
                throw new PaymentNotFoundException(paymentId);
            return _mapper.Map<Payment, PaymentToReadDto>(payment);
        }

        public async Task<IEnumerable<PaymentToReadDto>> GetPaymentsByUserSSNAsync(string userSSN)
        {
            var payments = await _unitOfWork.PaymentRepository.GetByUserSSNAsync(userSSN);
            return _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentToReadDto>>(payments);

        }

    }
}
