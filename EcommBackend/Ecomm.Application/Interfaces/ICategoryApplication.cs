using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Category.Request;
using Ecomm.Application.Dtos.Category.Response;

namespace Ecomm.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<IEnumerable<CategoryResponseDto>>> ListCategories(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories();
        Task<BaseResponse<CategoryResponseDto>> CategoryById(Guid categoryId);
        Task<BaseResponse<bool>> CreateCategory(CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateCategory(Guid categoryId, CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteCategory(Guid categoryId);
    }
}
