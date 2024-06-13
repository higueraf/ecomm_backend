using AutoMapper;
using Ecomm.Application.Commons.Bases.Response;
using Ecomm.Application.Dtos.Role.Request;
using Ecomm.Application.Dtos.Role.Response;
using Ecomm.Domain.Entities;
using Ecomm.Utils.Static;

namespace Ecomm.Application.Mappers
{
    public class RoleMappingsProfile : Profile
    {
        public RoleMappingsProfile() 
        { 
            CreateMap<Role, RoleResponseDto>()
                .ForMember(x => x.RoleId, x => x.MapFrom(y => y.Id ))
                .ForMember(c => c.StateRole, c => c.MapFrom(s=> s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<Role>, BaseEntityResponse<RoleResponseDto>>()
                .ReverseMap();
            CreateMap<RoleRequestDto, Role>();
            CreateMap<Role, RoleSelectResponseDto>()
                .ForMember(x => x.RoleId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
