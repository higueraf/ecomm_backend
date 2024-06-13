using AutoMapper;
using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.User.Request;
using Ecomm.Application.Dtos.User.Response;
using Ecomm.Application.Interfaces;
using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Commons.Ordering;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Utils.Static;
using Event.Application.Validators.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Ecomm.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;

        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var user = _mapper.Map<User>(requestDto);
            user.Password = BC.HashPassword(user.Password);
            response.Data = await  _unitOfWork.User.CreateAsync(user);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_CREATE;
            } else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<TokenResponseDto>> GenerateToken(TokenRequestDto requestDto)
        {
            var response = new BaseResponse<TokenResponseDto>();
            var user = await _unitOfWork.User.AccountByEmail(requestDto.Email!);
            if (user is not null && BC.Verify(requestDto.Password, user.Password))
            {
                response.IsSuccess = true;
                var tokenResponse = new TokenResponseDto();
                tokenResponse.UserId = user.Id;
                tokenResponse.UserName = user.UserName;
                tokenResponse.Email = user.Email;
                tokenResponse.Role = user.Role!.Name;
                tokenResponse.Token = GenerateToken(user);
                response.Data = tokenResponse;
                response.Message = ReplyMessage.MESSAGE_TOKEN;
            } 
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
            }
            return response;
        }
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email!),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role, user.Role!.Name!)

            }; 
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"]!)),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<BaseResponse<IEnumerable<UserResponseDto>>> ListUsers(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<UserResponseDto>>();
            try
            {
                var users = _unitOfWork.User.GetAllQueryable();
                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            users = users.Where(c => c.UserName!.Contains(filters.TextFilter));
                            break;
                        case 2:
                            users = users.Where(c => c.UserName!.Contains(filters.TextFilter));
                            break;

                    }
                }
                if (filters.StateFilter is not null)
                {
                    users = users.Where(category => category.State!.Equals(filters.StateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    users = users.Where(c => c.CreateDate >= Convert.ToDateTime(filters.StartDate) && c.CreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));

                }
                if (filters.Sort is not null) filters.Sort = "Id";
                var items = await _orderingQuery.Ordering(filters, users, !(bool)filters.Download!).ToListAsync();
                response.TotalRecords = await users.CountAsync();


                response.IsSuccess = true;
                response.TotalRecords = await users.CountAsync();
                response.Data = _mapper.Map<IEnumerable<UserResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;

        }

        public async Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUsers()
        {
            var response = new BaseResponse<IEnumerable<UserSelectResponseDto>>();
            var users = await _unitOfWork.User.GetAllAsync();
            if (users is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<UserSelectResponseDto>>(users);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<UserResponseDto>> UserById(Guid userId)
        {
            var response = new BaseResponse<UserResponseDto>();
            var user = await _unitOfWork.User.GetByIdAsync(userId);
            if (user is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<UserResponseDto>(user);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> CreateUser(UserRequestDto requestDto)
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
            var user = _mapper.Map<User>(requestDto);
            response.Data = await _unitOfWork.User.CreateAsync(user);
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
        public async Task<BaseResponse<bool>> UpdateUser(Guid userId, UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var userEdit = await UserById(userId);
            if (userEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            var user = _mapper.Map<User>(requestDto);
            user.Id = userId;
            response.Data = await _unitOfWork.User.UpdateAsync(user);
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



        public async Task<BaseResponse<bool>> DeleteUser(Guid userId)
        {
            var response = new BaseResponse<bool>();
            var userEdit = await UserById(userId);
            if (userEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.User.DeleteAsync(userId);
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
