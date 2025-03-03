using Guardian.Application.Contracts;
using Guardian.Domain.Models;
using Guardian.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Persistence.Repositories
{
    public class UserRepository : IUserRepository<ApplicationUser>
    {
        private readonly GuardianDatabaseContext _context;

        public UserRepository(GuardianDatabaseContext context)
        {
            _context = context;
        }
        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAsync()
        {
            // use database context to communicate to the database
            return await _context.Users.ToListAsync();
        }

        public Task GetByIdAsync(int id)
        {
            // use database context to communicate with the database
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
