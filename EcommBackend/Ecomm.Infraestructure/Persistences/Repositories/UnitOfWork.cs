using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Persistences.Contexts;
using Ecomm.Infraestructure.Persistences.Interfaces;


namespace Ecomm.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommContext _context;

        public IGenericRepository<Category> _category = null!;
        public IGenericRepository<Order> _order= null!;
        public IGenericRepository<PaymentMethod> _paymentMethod = null!;
        public IGenericRepository<Product> _product = null!;
        public IGenericRepository<Role> _role = null!;
        public IUserRepository _user= null!;
        

        public UnitOfWork(EcommContext context)
        {
            _context = context;
        }
        public IGenericRepository<Category> Category => _category ?? new GenericRepository<Category>(_context);
        public IGenericRepository<Order> Order => _order?? new GenericRepository<Order>(_context);
        public IGenericRepository<PaymentMethod> PaymentMethod => _paymentMethod ?? new GenericRepository<PaymentMethod>(_context);
        public IGenericRepository<Product> Product => _product ?? new GenericRepository<Product>(_context);
        public IGenericRepository<Role> Role => _role ?? new GenericRepository<Role>(_context);
        public IUserRepository User => _user ?? new UserRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChangers()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangersAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
