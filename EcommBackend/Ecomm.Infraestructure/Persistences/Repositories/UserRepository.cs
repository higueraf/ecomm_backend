using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Persistences.Contexts;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Infraestructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly EcommContext _context;
        public UserRepository(EcommContext context) : base(context)
        {
            _context = context;

        }
        async Task<User> IUserRepository.AccountByEmail(string email)
        {
            var account = await _context.User.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email!.Equals(email));
            return account!;
        }
    }
}
