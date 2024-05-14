
namespace Ecomm.Application.Dtos.Product.Response
{
    public class ProductResponseDto
    {
        public Guid ProductId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Stock { get; set; }
        public string? Image { get; set; }
        public string? Price { get; set; }
        public string? Category { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }
        public string? StateProduct{ get; set; }
    }
}
