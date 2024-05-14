using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Role.Request;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleApplication _categoryApplication;

        public RoleController(IRoleApplication categoryApplication = null)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListRoles([FromBody] BaseFilterRequest filters)
        {
            var response = await _categoryApplication.ListRoles(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectRoles()
        {
            var response = await _categoryApplication.ListSelectRoles();
            return Ok(response);
        }
        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> RoleById(Guid categoryId)
        {
            var response = await _categoryApplication.RoleById(categoryId);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequestDto requestDto)
        {
            var response = await _categoryApplication.CreateRole(requestDto);
            return Ok(response);
        }
        [HttpPut("Update/{categoryId:int}")]
        public async Task<IActionResult> UpdateRole(Guid categoryId, [FromBody] RoleRequestDto requestDto)
        {
            var response = await _categoryApplication.UpdateRole(categoryId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{categoryId:int}")]
        public async Task<IActionResult> DeleteRole(Guid categoryId)
        {
            var response = await _categoryApplication.DeleteRole(categoryId);
            return Ok(response);
        }

    }

}
