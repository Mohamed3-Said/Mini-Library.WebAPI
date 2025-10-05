using DomainLayer.Models.BookModule;
using DomainLayer.Models.EmployeeModule;
using DomainLayer.Models.PaymentModule.Enums;
using DomainLayer.Models.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.PaymentModule
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        // العلاقة بالمستخدم (Primary key في User هو SSN)
        [Required]
        public string UserSSN { get; set; } = default!;
        public User? User { get; set; }

        // لو الدفع مرتبط بسجل استعارة (اختياري)
        public int? UserBorrowId { get; set; }
        public UserBorrow? UserBorrow { get; set; }


        // المبلغ الأساسي (مثلاً رسوم الاستعارة أو الغرامة)
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999999999999.99)]
        public decimal Amount { get; set; }

        // مصاريف التوصيل إن موجودة
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999999999999.99)]
        public decimal DeliveryFee { get; set; } = 0m;

        // أي رسوم بوابة دفع (موجودة لو Online)
        [Column(TypeName = "decimal(18,2)")]
        public decimal GatewayFee { get; set; } = 0m;

        // المبلغ الإجمالي (Amount + DeliveryFee + GatewayFee)
        // المجموع النهائي (نخزنه كنسخة للتدقيق)
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; } = "EGP";

        [Required]
        public PaymentMethod Method { get; set; } = PaymentMethod.Cash;

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        // معرف المعاملة من بوابة الدفع (Transaction reference)
        [StringLength(200)]
        public string? TransactionId { get; set; }

        // لو الدفع تم التأكيد بواسطة موظف (cash/manual confirm)
        public int? ConfirmedByEmployeeId { get; set; }
        public Employee? ConfirmedByEmployee { get; set; }

        // توصيل
        public bool IsDelivery { get; set; } = false;
        [StringLength(500)]
        public string? DeliveryAddress { get; set; }


        // تاريخ الإنشاء و آخر تحديث
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
