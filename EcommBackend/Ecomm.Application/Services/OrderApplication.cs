using AutoMapper;
using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Order.Request;
using Ecomm.Application.Dtos.Order.Response;
using Ecomm.Application.Dtos.Product.Response;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Validators.Order;
using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Commons.Ordering;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Utils.Static;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Application.Services
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly OrderValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;

        public OrderApplication(IUnitOfWork unitOfWork, IMapper mapper, OrderValidator validationRules, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<IEnumerable<OrderResponseDto>>> ListOrders(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<OrderResponseDto>>();
            try
            {
                var orders = _unitOfWork.Order.GetAllQueryable();
                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            orders = orders.Where(c => c.Client!.UserName!.Contains(filters.TextFilter));
                            break;
                        case 2:
                            orders = orders.Where(c => c.Client!.Email.Contains(filters.TextFilter));
                            break;

                    }
                }
                if (filters.StateFilter is not null)
                {
                    orders = orders.Where(category => category.State!.Equals(filters.StateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    orders = orders.Where(c => c.CreateDate >= Convert.ToDateTime(filters.StartDate) && c.CreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));

                }
                if (filters.Sort is not null) filters.Sort = "Id";
                var items = await _orderingQuery.Ordering(filters, orders, !(bool)filters.Download!).ToListAsync();
                response.IsSuccess = true;
                response.TotalRecords = await orders.CountAsync();
                response.Data = _mapper.Map<IEnumerable<OrderResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
                
            } catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }
            
            return response;
        }

        public async Task<BaseResponse<IEnumerable<OrderSelectResponseDto>>> ListSelectOrders()
        {
            var response = new BaseResponse<IEnumerable<OrderSelectResponseDto>>();
            var orders = await _unitOfWork.Order.GetAllAsync();
            if (orders is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<OrderSelectResponseDto>>(orders);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<OrderResponseDto>> OrderById(Guid categoryId)
        {
            var response = new BaseResponse<OrderResponseDto>();
            var category = await _unitOfWork.Order.GetByIdAsync(categoryId);
            if (category is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<OrderResponseDto>(category);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> CreateOrder(OrderRequestDto requestDto)
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
            var category = _mapper.Map<Order>(requestDto);
            response.Data = await _unitOfWork.Order.CreateAsync(category);
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
        public async Task<BaseResponse<bool>> UpdateOrder(Guid categoryId, OrderRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await OrderById(categoryId);
            if (categoryEdit is null) { 
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            var category = _mapper.Map<Order>(requestDto);
            category.Id = categoryId;
            response.Data = await _unitOfWork.Order.UpdateAsync(category);
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

        

        public async Task<BaseResponse<bool>> DeleteOrder(Guid categoryId)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await OrderById(categoryId);
            if (categoryEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.Order.DeleteAsync(categoryId);
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
