﻿namespace Ecomm.Domain.Entities;

public partial class Role : BaseEntity

{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<User> Users{ get; set; } = new List<User>();
}
