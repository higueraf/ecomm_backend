using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.PaymentMethod.Request;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodApplication _categoryApplication;

        public PaymentMethodController(IPaymentMethodApplication categoryApplication = null)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListPaymentMethods([FromBody] BaseFilterRequest filters)
        {
            var response = await _categoryApplication.ListPaymentMethods(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectPaymentMethods()
        {
            var response = await _categoryApplication.ListSelectPaymentMethods();
            return Ok(response);
        }
        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> PaymentMethodById(Guid categoryId)
        {
            var response = await _categoryApplication.PaymentMethodById(categoryId);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] PaymentMethodRequestDto requestDto)
        {
            var response = await _categoryApplication.CreatePaymentMethod(requestDto);
            return Ok(response);
        }
        [HttpPut("Update/{categoryId:int}")]
        public async Task<IActionResult> UpdatePaymentMethod(Guid categoryId, [FromBody] PaymentMethodRequestDto requestDto)
        {
            var response = await _categoryApplication.UpdatePaymentMethod(categoryId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{categoryId:int}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid categoryId)
        {
            var response = await _categoryApplication.DeletePaymentMethod(categoryId);
            return Ok(response);
        }

    }

}
