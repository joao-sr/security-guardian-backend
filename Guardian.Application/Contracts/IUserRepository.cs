
using Guardian.Domain.Models;

namespace Guardian.Application.Contracts
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        // user repository will implement only the methods from Generic repository
    }
}
