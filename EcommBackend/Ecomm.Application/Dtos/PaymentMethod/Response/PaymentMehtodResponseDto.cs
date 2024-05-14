namespace Ecomm.Application.Dtos.PaymentMethod.Response
{
    public class PaymentMethodResponseDto
    {
        public int PaymentMethodId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? State { get; set; }
        public string? StatePaymentMethod{ get; set; }
    }
}
