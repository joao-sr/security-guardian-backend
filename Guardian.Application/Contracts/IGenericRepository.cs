using Guardian.Domain.Models;

namespace Guardian.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAsync();
        Task GetByIdAsync(int id);
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
