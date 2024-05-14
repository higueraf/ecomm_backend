using System;
using System.Collections.Generic;

namespace Ecomm.Domain.Entities;

public partial class ProductPhoto : BaseEntity
{
    public int ProductId { get; set; }

    public string? Url { get; set; }

    public virtual Product  product { get; set; } = null!;

}
