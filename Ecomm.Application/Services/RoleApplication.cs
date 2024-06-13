using AutoMapper;
using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Role.Request;
using Ecomm.Application.Dtos.Role.Response;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Validators.Role;
using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Commons.Ordering;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Utils.Static;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Application.Services
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;

        public RoleApplication(IUnitOfWork unitOfWork, IMapper mapper, RoleValidator validationRules, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<IEnumerable<RoleResponseDto>>> ListRoles(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<RoleResponseDto>>();
            try
            {
                var categories = _unitOfWork.Role.GetAllQueryable();
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
                response.Data = _mapper.Map<IEnumerable<RoleResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<RoleSelectResponseDto>>> ListSelectRoles()
        {
            var response = new BaseResponse<IEnumerable<RoleSelectResponseDto>>();
            var categories = await _unitOfWork.Role.GetAllAsync();
            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<RoleSelectResponseDto>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<RoleResponseDto>> RoleById(Guid categoryId)
        {
            var response = new BaseResponse<RoleResponseDto>();
            var category = await _unitOfWork.Role.GetByIdAsync(categoryId);
            if (category is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<RoleResponseDto>(category);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> CreateRole(RoleRequestDto requestDto)
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
            var category = _mapper.Map<Role>(requestDto);
            response.Data = await _unitOfWork.Role.CreateAsync(category);
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
        public async Task<BaseResponse<bool>> UpdateRole(Guid categoryId, RoleRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await RoleById(categoryId);
            if (categoryEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            var category = _mapper.Map<Role>(requestDto);
            category.Id = categoryId;
            response.Data = await _unitOfWork.Role.UpdateAsync(category);
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



        public async Task<BaseResponse<bool>> DeleteRole(Guid categoryId)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await RoleById(categoryId);
            if (categoryEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.Role.DeleteAsync(categoryId);
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
