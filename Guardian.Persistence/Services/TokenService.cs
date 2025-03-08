using Guardian.Application.Contracts;
using Guardian.Domain;
using Guardian.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Guardian.Persistence.Services
{
    public class TokenService:ITokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }

        /// <summary>
        /// Receives a user object and it will get user related claims from the database and roles associated to the user
        /// generates a claim list with the information
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GenerateToken()
        {
            var user = new ApplicationUser();
            // if user is not provided the usermanager triggers some error and skips
            // make suer the user is not empty
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            

            return "yea";
        }


    }
}
