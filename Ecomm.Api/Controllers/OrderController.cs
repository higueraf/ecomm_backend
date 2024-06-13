using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Order.Request;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderApplication _categoryApplication;

        public OrderController(IOrderApplication categoryApplication = null)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListOrders([FromBody] BaseFilterRequest filters)
        {
            var response = await _categoryApplication.ListOrders(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectOrders()
        {
            var response = await _categoryApplication.ListSelectOrders();
            return Ok(response);
        }
        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> OrderById(Guid categoryId)
        {
            var response = await _categoryApplication.OrderById(categoryId);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto requestDto)
        {
            var response = await _categoryApplication.CreateOrder(requestDto);
            return Ok(response);
        }
        [HttpPut("Update/{categoryId:int}")]
        public async Task<IActionResult> UpdateOrder(Guid categoryId, [FromBody] OrderRequestDto requestDto)
        {
            var response = await _categoryApplication.UpdateOrder(categoryId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{categoryId:int}")]
        public async Task<IActionResult> DeleteOrder(Guid categoryId)
        {
            var response = await _categoryApplication.DeleteOrder(categoryId);
            return Ok(response);
        }

    }

}
