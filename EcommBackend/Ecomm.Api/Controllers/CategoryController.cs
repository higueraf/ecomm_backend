using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Category.Request;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication = null)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListCategories([FromBody] BaseFilterRequest filters)
        {
            var response = await _categoryApplication.ListCategories(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectCategories()
        {
            var response = await _categoryApplication.ListSelectCategories();
            return Ok(response);
        }
        [HttpGet("{categoryId:Guid}")]
        public async Task<IActionResult> CategoryById(Guid categoryId)
        {
            var response = await _categoryApplication.CategoryById(categoryId);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.CreateCategory(requestDto);
            return Ok(response);
        }
        [HttpPut("Update/{categoryId:Guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.UpdateCategory(categoryId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{categoryId:Guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var response = await _categoryApplication.DeleteCategory(categoryId);
            return Ok(response);
        }

    }

}
