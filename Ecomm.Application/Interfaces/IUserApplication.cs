using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.User.Request;
using Ecomm.Application.Dtos.User.Response;

namespace Ecomm.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto);
        Task<BaseResponse<TokenResponseDto>> GenerateToken(TokenRequestDto requestDto);
        Task<BaseResponse<IEnumerable<UserResponseDto>>> ListUsers(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUsers();
        Task<BaseResponse<UserResponseDto>> UserById(Guid userId);
        Task<BaseResponse<bool>> CreateUser(UserRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateUser(Guid userId, UserRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteUser(Guid userId);

    }
}
