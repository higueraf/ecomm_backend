using AutoMapper;
using Ecomm.Application.Dtos.User.Request;
using Ecomm.Domain.Entities;

namespace Ecomm.Application.Mappers
{
    public class UserMappingsProfile : Profile
    {
        public UserMappingsProfile() 
        {
            CreateMap<UserRequestDto, User>();
            CreateMap<TokenRequestDto, User>();
        }
    }
}
