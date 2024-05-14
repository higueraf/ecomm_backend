using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Product.Request;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication = null)
        {
            _productApplication = productApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListProducts([FromBody] BaseFilterRequest filters)
        {
            var response = await _productApplication.ListProducts(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectProducts()
        {
            var response = await _productApplication.ListSelectProducts();
            return Ok(response);
        }
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(Guid productId)
        {
            var response = await _productApplication.ProductById(productId);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequestDto requestDto)
        {
            var response = await _productApplication.CreateProduct(requestDto);
            return Ok(response);
        }
        [HttpPut("Update/{productId:int}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.UpdateProduct(productId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var response = await _productApplication.DeleteProduct(productId);
            return Ok(response);
        }

    }

}
