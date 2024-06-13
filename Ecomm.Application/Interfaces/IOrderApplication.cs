using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Order.Request;
using Ecomm.Application.Dtos.Order.Response;
using Ecomm.Application.Dtos.Product.Response;

namespace Ecomm.Application.Interfaces
{
    public interface IOrderApplication
    {
        Task<BaseResponse<IEnumerable<OrderResponseDto>>> ListOrders(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<OrderSelectResponseDto>>> ListSelectOrders();
        Task<BaseResponse<OrderResponseDto>> OrderById(Guid categoryId);
        Task<BaseResponse<bool>> CreateOrder(OrderRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateOrder(Guid categoryId, OrderRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteOrder(Guid categoryId);
    }
}
