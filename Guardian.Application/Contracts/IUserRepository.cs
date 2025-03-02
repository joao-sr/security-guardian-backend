
using Guardian.Domain.Models;

namespace Guardian.Application.Contracts
{
    public interface IUserRepository<T>
    {
        Task<IReadOnlyList<ApplicationUser>> GetAsync();
        Task GetByIdAsync(int id);
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
