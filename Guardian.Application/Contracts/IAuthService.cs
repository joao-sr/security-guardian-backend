using Guardian.Domain.Requests;
using Guardian.Domain.Responses;

namespace Guardian.Application.Contracts
{
    public interface IAuthService
    {
        public Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request);
        public Task Login(LoginRequest model);
    }

    
}
