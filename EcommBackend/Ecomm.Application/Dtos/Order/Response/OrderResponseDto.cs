
namespace Ecomm.Application.Dtos.Order.Response
{
    public class OrderResponseDto
    {
        public Guid? OrderId { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Total { get; set; }
        public int? State { get; set; }
        public string? StateOrder{ get; set; }
    }
}
