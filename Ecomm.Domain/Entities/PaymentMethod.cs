namespace Ecomm.Domain.Entities;

public partial class PaymentMethod : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }

}
