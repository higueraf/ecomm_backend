using System;
using System.Collections.Generic;

namespace Ecomm.Domain.Entities;

public partial class Product : BaseEntity
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Stock { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public bool Iva { get; set; }   
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
