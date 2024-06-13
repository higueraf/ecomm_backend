
using Ecomm.Domain.Entities;

namespace Ecomm.Infraestructure.Persistences.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> AccountByEmail(string email);
    }
}
