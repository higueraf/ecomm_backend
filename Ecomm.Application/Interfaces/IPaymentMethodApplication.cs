using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.PaymentMethod.Request;
using Ecomm.Application.Dtos.PaymentMethod.Response;

namespace Ecomm.Application.Interfaces
{
    public interface IPaymentMethodApplication
    {
        Task<BaseResponse<IEnumerable<PaymentMethodResponseDto>>> ListPaymentMethods(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<PaymentMethodSelectResponseDto>>> ListSelectPaymentMethods();
        Task<BaseResponse<PaymentMethodResponseDto>> PaymentMethodById(Guid categoryId);
        Task<BaseResponse<bool>> CreatePaymentMethod(PaymentMethodRequestDto requestDto);
        Task<BaseResponse<bool>> UpdatePaymentMethod(Guid categoryId, PaymentMethodRequestDto requestDto);
        Task<BaseResponse<bool>> DeletePaymentMethod(Guid categoryId);
    }
}
