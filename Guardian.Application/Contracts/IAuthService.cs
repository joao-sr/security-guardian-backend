
using Guardian.Domain;

namespace Guardian.Application.Contracts
{
    public interface IAuthService
    {
        public Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request);
    }
}
