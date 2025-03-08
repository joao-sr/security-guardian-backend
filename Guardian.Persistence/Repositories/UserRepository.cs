using Guardian.Application.Contracts;
using Guardian.Domain.Models;
using Guardian.Persistence.DatabaseContext;

namespace Guardian.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GuardianDatabaseContext _context;

        public UserRepository(GuardianDatabaseContext context)
        {
            _context = context;
        }
    }
}
