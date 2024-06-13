using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Product.Request;
using Ecomm.Application.Dtos.Product.Response;

namespace Ecomm.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProducts();
        Task<BaseResponse<ProductResponseByIdDto>> ProductById(Guid productId);
        Task<BaseResponse<bool>> CreateProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateProduct(Guid productId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteProduct(Guid productId);
    }
}
