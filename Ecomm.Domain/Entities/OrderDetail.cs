using System;
using System.Collections.Generic;

namespace Ecomm.Domain.Entities;

public partial class OrderDetail : BaseEntity
{
    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Qty { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }
    public decimal? TotalDetail { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Order? Sale { get; set; }
}
