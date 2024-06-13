namespace Ecomm.Domain.Entities;

public partial class User : BaseEntity
{
    public User()
    {
        Orders = new HashSet<Order>();
    }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public Guid? RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; }
}
