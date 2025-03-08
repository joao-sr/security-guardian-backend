
using Guardian.Domain.Models;

namespace Guardian.Application.Contracts
{
    public interface IUserRepository
    {
        // since i am using the same database for managing users
        // user repository does not inherit from the generic repository
        // it will implement its own methods
    }
}
