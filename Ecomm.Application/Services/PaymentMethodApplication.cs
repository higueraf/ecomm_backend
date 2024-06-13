using AutoMapper;
using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.PaymentMethod.Request;
using Ecomm.Application.Dtos.PaymentMethod.Response;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Validators.PaymentMethod;
using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Commons.Ordering;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Utils.Static;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Application.Services
{
    public class PaymentMethodApplication : IPaymentMethodApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaymentMethodValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;

        public PaymentMethodApplication(IUnitOfWork unitOfWork, IMapper mapper, PaymentMethodValidator validationRules, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<IEnumerable<PaymentMethodResponseDto>>> ListPaymentMethods(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<PaymentMethodResponseDto>>();
            try
            {
                var categories = _unitOfWork.PaymentMethod.GetAllQueryable();
                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            categories = categories.Where(c => c.Name!.Contains(filters.TextFilter));
                            break;
                        case 2:
                            categories = categories.Where(c => c.Description!.Contains(filters.TextFilter));
                            break;

                    }
                }
                if (filters.StateFilter is not null)
                {
                    categories = categories.Where(category => category.State!.Equals(filters.StateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    categories = categories.Where(c => c.CreateDate >= Convert.ToDateTime(filters.StartDate) && c.CreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));

                }
                if (filters.Sort is not null) filters.Sort = "Id";
                var items = await _orderingQuery.Ordering(filters, categories, !(bool)filters.Download!).ToListAsync();
                response.IsSuccess = true;
                response.TotalRecords = await categories.CountAsync();
                response.Data = _mapper.Map<IEnumerable<PaymentMethodResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<PaymentMethodSelectResponseDto>>> ListSelectPaymentMethods()
        {
            var response = new BaseResponse<IEnumerable<PaymentMethodSelectResponseDto>>();
            var categories = await _unitOfWork.PaymentMethod.GetAllAsync();
            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<PaymentMethodSelectResponseDto>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<PaymentMethodResponseDto>> PaymentMethodById(Guid categoryId)
        {
            var response = new BaseResponse<PaymentMethodResponseDto>();
            var category = await _unitOfWork.PaymentMethod.GetByIdAsync(categoryId);
            if (category is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<PaymentMethodResponseDto>(category);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> CreatePaymentMethod(PaymentMethodRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }
            var category = _mapper.Map<PaymentMethod>(requestDto);
            response.Data = await _unitOfWork.PaymentMethod.CreateAsync(category);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_CREATE;

            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> UpdatePaymentMethod(Guid categoryId, PaymentMethodRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await PaymentMethodById(categoryId);
            if (categoryEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            var category = _mapper.Map<PaymentMethod>(requestDto);
            category.Id = categoryId;
            response.Data = await _unitOfWork.PaymentMethod.UpdateAsync(category);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }



        public async Task<BaseResponse<bool>> DeletePaymentMethod(Guid categoryId)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await PaymentMethodById(categoryId);
            if (categoryEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.PaymentMethod.DeleteAsync(categoryId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }


    }
}
