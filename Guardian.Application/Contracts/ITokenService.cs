using Guardian.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Guardian.Application.Contracts
{
    public interface ITokenService
    {
        Task<string> GenerateToken();
    }
}
