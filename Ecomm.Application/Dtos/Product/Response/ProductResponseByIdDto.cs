
namespace Ecomm.Application.Dtos.Product.Response
{
    public class ProductResponseByIdDto
    {
        public Guid ProductId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Stock { get; set; }
        public string? Image { get; set; }
        public string? Price { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? Iva { get; set; }
        public int? State { get; set; }
    }
}
