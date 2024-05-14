using Ecomm.Domain.Entities;


namespace Ecomm.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> Category { get; }
        IGenericRepository<Order> Order{ get; }
        IGenericRepository<PaymentMethod> PaymentMethod { get; }
        IGenericRepository<Product> Product { get; }
        IGenericRepository<Role> Role { get; }

        IUserRepository User { get; }
        void SaveChangers();
        Task SaveChangersAsync();

    }
}
