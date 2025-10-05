using DomainLayer.Models.PaymentModule;
using DomainLayer.Models.PaymentModule.Enums;
using Shared.DataTransfareObjects.PaymentModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.PaymentModuleProfile
{
    public class PaymentProfile : AutoMapper.Profile
    {
        public PaymentProfile()
        {
            CreateMap<CreatePaymentDto, Payment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.PaymentDate))
                  .ForMember(dest => dest.Method,
                       opt => opt.MapFrom(src => Enum.Parse<PaymentMethod>(src.Method, true)))
            .ForMember(dest => dest.Status,
                       opt => opt.MapFrom(src => Enum.Parse<PaymentStatus>(src.Status, true)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PaymentStatus.Pending)) // Override
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Amount + src.DeliveryFee + src.GatewayFee))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));


            CreateMap<UpdatePaymentDto, Payment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                  .ForMember(dest => dest.Method,
                       opt => opt.MapFrom(src => Enum.Parse<PaymentMethod>(src.Method, true)))
            .ForMember(dest => dest.Status,
                       opt => opt.MapFrom(src => Enum.Parse<PaymentStatus>(src.Status, true)))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PaymentStatus.Pending)) // Override
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Amount + src.DeliveryFee + src.GatewayFee))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));


            CreateMap<Payment, PaymentToReadDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Method,
                       opt => opt.MapFrom(src => src.Method.ToString()))
            .ForMember(dest => dest.Status,
                       opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.DeliveryFee, opt => opt.MapFrom(src => src.DeliveryFee))
            .ForMember(dest => dest.GatewayFee, opt => opt.MapFrom(src => src.GatewayFee))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.IsDelivery, opt => opt.MapFrom(src => src.IsDelivery))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));


        }

    }
}
