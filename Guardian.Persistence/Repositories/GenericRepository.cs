using Guardian.Application.Contracts;

namespace Guardian.Persistence.Repositories
{
    public class GenericRepository : IGenericRepository<T> where T : BaseEntity
    {
        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
