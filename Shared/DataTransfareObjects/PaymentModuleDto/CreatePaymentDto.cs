using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObjects.PaymentModuleDto
{
    public class CreatePaymentDto
    {
        public string UserSSN { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string Method { get; set; }
        public string Status { get; set; }


        // مصاريف اختيارية
        public decimal DeliveryFee { get; set; } = 0m;
        public decimal GatewayFee { get; set; } = 0m;

        // العملة
        public string Currency { get; set; } = "EGP";


        // اختياري لو توصيل
        public bool IsDelivery { get; set; } = false;
        public string? DeliveryAddress { get; set; }

        // Online Transaction
        public string? TransactionId { get; set; }

    }
}
