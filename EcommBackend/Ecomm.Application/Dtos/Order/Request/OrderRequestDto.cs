using Microsoft.AspNetCore.Http;

namespace Ecomm.Application.Dtos.Order.Request
{
    public class OrderRequestDto
    {
        public Guid? ClientId { get; set; }

        public Guid? PaymentMethodId { get; set; }

        public DateTime? OrderDate { get; set; }
        public decimal? SubTotal { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Total { get; set; }
    }
}
