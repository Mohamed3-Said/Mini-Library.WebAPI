using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.PaymentModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //// GET: api/payment
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PaymentToReadDto>>>> GetAllPaymentAsync()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(new ApiResponse<IEnumerable<PaymentToReadDto>>(payments, "Payments retrieved successfully"));
        }
        // GET: api/payment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PaymentToReadDto>>> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            return Ok(new ApiResponse<PaymentToReadDto>(payment! , "Payment retrieved successfully"));
        }
        // GET: api/payment/user/{ssn}
        [HttpGet("user/{ssn}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PaymentToReadDto>>>> GetPaymentsByUserSSNAsync(string ssn)
        {
            var payments = await _paymentService.GetPaymentsByUserSSNAsync(ssn);
            return Ok(new ApiResponse<IEnumerable<PaymentToReadDto>>(payments, "Payments retrieved successfully"));
        }
        // POST: api/payment //Create Payment
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PaymentToReadDto>>> CreatePaymentAsync([FromBody] CreatePaymentDto createPaymentDto)
        {
            var created = await _paymentService.CreatePaymentAsync(createPaymentDto);

            return Ok(
                new ApiResponse<PaymentToReadDto>(created, "Payment created successfully")
            );
        }


        // PUT: api/payment //Update Payment
        [HttpPut]
        public async Task<ActionResult<ApiResponse<PaymentToReadDto>>> UpdatePaymentAsync([FromBody] UpdatePaymentDto updatePaymentDto)
        {
            var updated = await _paymentService.UpdatePaymentAsync(updatePaymentDto);
            return Ok(new ApiResponse<PaymentToReadDto>(updated, "Payment updated successfully"));
        }


        // DELETE: api/payment/{id} //Delete Payment
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePaymentAsync(int id )
        {
            var deleted =await _paymentService.DeletePaymentAsync(id);
            return Ok(new ApiResponse<bool>(true, "Payment deleted successfully"));
        }

    }
}
