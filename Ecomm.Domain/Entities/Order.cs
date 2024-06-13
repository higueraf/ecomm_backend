using System;
using System.Collections.Generic;

namespace Ecomm.Domain.Entities;

public partial class Order : BaseEntity
{
    public Guid? OrderId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? PaymentMethodId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? Tax { get; set; }
    public decimal? Total { get; set; }
    public virtual User? Client { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
