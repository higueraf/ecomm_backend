using AutoMapper;
using Ecomm.Application.Commons.Bases.Response;
using Ecomm.Application.Dtos.Category.Request;
using Ecomm.Application.Dtos.Category.Response;
using Ecomm.Domain.Entities;
using Ecomm.Utils.Static;

namespace Ecomm.Application.Mappers
{
    public class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile() 
        { 
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id ))
                .ForMember(c => c.StateCategory, c => c.MapFrom(s=> s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category, CategorySelectResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
