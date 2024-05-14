using AutoMapper;
using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Product.Request;
using Ecomm.Application.Dtos.Product.Response;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Validators.Product;
using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Commons.Ordering;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Utils.Static;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;
        private readonly IFileStorageLocalApplication _fileStorage;
        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductValidator validationRules, IOrderingQuery orderingQuery, IFileStorageLocalApplication fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _orderingQuery = orderingQuery;
            _fileStorage = fileStorage;
        }


        public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<ProductResponseDto>>();
            try
            {
                var products = _unitOfWork.Product.GetAllQueryable();
                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            products = products.Where(c => c.Name!.Contains(filters.TextFilter));
                            break;
                        case 2:
                            products = products.Where(c => c.Description!.Contains(filters.TextFilter));
                            break;

                    }
                }
                if (filters.StateFilter is not null)
                {
                    products = products.Where(product => product.State!.Equals(filters.StateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    products = products.Where(c => c.CreateDate >= Convert.ToDateTime(filters.StartDate) && c.CreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));

                }
                if (filters.Sort is not null) filters.Sort = "Id";
                var items = await _orderingQuery.Ordering(filters, products, !(bool)filters.Download!).ToListAsync();
                response.IsSuccess = true;
                response.TotalRecords = await products.CountAsync();
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProducts()
        {
            var response = new BaseResponse<IEnumerable<ProductSelectResponseDto>>();
            var products = await _unitOfWork.Product.GetAllAsync();
            if (products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProductSelectResponseDto>>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<ProductResponseByIdDto>> ProductById(Guid productId)
        {
            var response = new BaseResponse<ProductResponseByIdDto>();
            var product = await _unitOfWork.Product.GetByIdAsync(productId);
            if (product is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductResponseByIdDto>(product);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> CreateProduct(ProductRequestDto requestDto)
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
            var product = _mapper.Map<Product>(requestDto);
            if(requestDto.Image is not null)
            {
                product.Image = await _fileStorage.SaveFile(FileDestination.PRODUCTS, requestDto.Image);
            }
            try
            {
                
                response.Data = await _unitOfWork.Product.CreateAsync(product);
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
            } catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> UpdateProduct(Guid productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var productEdit = await ProductById(productId);
            if (productEdit is null) { 
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }
            var product = _mapper.Map<Product>(requestDto);
            if (requestDto.Image is not null)
            {
                product.Image = await _fileStorage.EditFile(FileDestination.PRODUCTS, requestDto.Image, productEdit.Data!.Image!);
            } else
            {
                product.Image = productEdit.Data!.Image!;
            }
            product.Id = productId;
            response.Data = await _unitOfWork.Product.UpdateAsync(product);
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

        

        public async Task<BaseResponse<bool>> DeleteProduct(Guid productId)
        {
            var response = new BaseResponse<bool>();
            var productEdit = await ProductById(productId);
            if (productEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.Product.DeleteAsync(productId);
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
