using Microsoft.AspNetCore.Http;

namespace Ecomm.Application.Dtos.Product.Request
{
    public class ProductRequestDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Stock { get; set; }
        public IFormFile? Image { get; set; }
        public string? Price { get; set; }
        public Guid? CategoryId { get; set; }
        public int? State { get; set; }
    }
}
