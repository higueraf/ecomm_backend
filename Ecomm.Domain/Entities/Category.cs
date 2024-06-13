namespace Ecomm.Domain.Entities;

public partial class Category : BaseEntity

{
    public string? Name { get; set; }

    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
