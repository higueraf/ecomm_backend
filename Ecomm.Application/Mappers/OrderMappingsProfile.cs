using AutoMapper;
using Ecomm.Application.Commons.Bases.Response;
using Ecomm.Application.Dtos.Order.Request;
using Ecomm.Application.Dtos.Order.Response;
using Ecomm.Application.Dtos.Product.Response;
using Ecomm.Domain.Entities;
using Ecomm.Utils.Static;

namespace Ecomm.Application.Mappers
{
    public class OrderMappingsProfile : Profile
    {
        public OrderMappingsProfile() 
        { 
            CreateMap<Order, OrderResponseDto>()
                .ForMember(x => x.OrderId, x => x.MapFrom(y => y.Id ))
                .ForMember(c => c.StateOrder, c => c.MapFrom(s=> s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<Order>, BaseEntityResponse<OrderResponseDto>>()
                .ReverseMap();
            CreateMap<OrderRequestDto, Order>();
            CreateMap<Order, OrderSelectResponseDto>()
                .ForMember(x => x.OrderId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
