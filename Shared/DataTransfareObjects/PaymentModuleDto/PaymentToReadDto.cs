using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.PaymentModuleDto
{
    public class PaymentToReadDto
    {
        public int PaymentId { get; set; }

        public string UserSSN { get; set; }

        public decimal Amount { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal GatewayFee { get; set; }

        public decimal TotalAmount { get; set; }

        public string Currency { get; set; }

        public string Method { get; set; }

        public string Status { get; set; }

        public bool IsDelivery { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
