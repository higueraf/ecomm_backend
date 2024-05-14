using AutoMapper;
using Ecomm.Application.Commons.Bases.Response;
using Ecomm.Application.Dtos.Product.Request;
using Ecomm.Application.Dtos.Product.Response;
using Ecomm.Domain.Entities;
using Ecomm.Utils.Static;

namespace Ecomm.Application.Mappers
{
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile() 
        { 
            CreateMap<Product, ProductResponseDto>()
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id ))
                .ForMember(x => x.Category, x => x.MapFrom(y => y.Category.Name))
                .ForMember(c => c.StateProduct, c => c.MapFrom(s=> s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<Product>, BaseEntityResponse<ProductResponseDto>>()
                .ReverseMap();
            CreateMap<ProductRequestDto, Product>();
            CreateMap<Product, ProductSelectResponseDto>()
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
            CreateMap<Product, ProductResponseByIdDto>()
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
