using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Role.Request;
using Ecomm.Application.Dtos.Role.Response;

namespace Ecomm.Application.Interfaces
{
    public interface IRoleApplication
    {
        Task<BaseResponse<IEnumerable<RoleResponseDto>>> ListRoles(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<RoleSelectResponseDto>>> ListSelectRoles();
        Task<BaseResponse<RoleResponseDto>> RoleById(Guid categoryId);
        Task<BaseResponse<bool>> CreateRole(RoleRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateRole(Guid categoryId, RoleRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteRole(Guid categoryId);
    }
}
