using AutoMapper;
using Ecomm.Application.Commons.Bases.Response;
using Ecomm.Application.Dtos.PaymentMethod.Request;
using Ecomm.Application.Dtos.PaymentMethod.Response;
using Ecomm.Domain.Entities;
using Ecomm.Utils.Static;

namespace Ecomm.Application.Mappers
{
    public class PaymentMethodMappingsProfile : Profile
    {
        public PaymentMethodMappingsProfile() 
        { 
            CreateMap<PaymentMethod, PaymentMethodResponseDto>()
                .ForMember(x => x.PaymentMethodId, x => x.MapFrom(y => y.Id ))
                .ForMember(c => c.StatePaymentMethod, c => c.MapFrom(s=> s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<PaymentMethod>, BaseEntityResponse<PaymentMethodResponseDto>>()
                .ReverseMap();
            CreateMap<PaymentMethodRequestDto, PaymentMethod>();
            CreateMap<PaymentMethod, PaymentMethodSelectResponseDto>()
                .ForMember(x => x.PaymentMethodId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
                

        }
    }
}
