﻿using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.User.Request;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication = null)
        {
            _userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequestDto requestDto)
        {
            var response = await _userApplication.GenerateToken(requestDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ListUsers([FromBody] BaseFilterRequest filters)
        {
            var response = await _userApplication.ListUsers(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectUsers()
        {
            var response = await _userApplication.ListSelectUsers();
            return Ok(response);
        }
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> UserById(Guid userId)
        {
            var response = await _userApplication.UserById(userId);
            return Ok(response);
        }
        [HttpPut("Update/{userId:int}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserRequestDto requestDto)
        {
            var response = await _userApplication.UpdateUser(userId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var response = await _userApplication.DeleteUser(userId);
            return Ok(response);
        }

    }

}
