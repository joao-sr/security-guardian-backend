using Guardian.Application.Contracts;
using Guardian.Domain;
using Guardian.Domain.Models;
using Guardian.Domain.Requests;
using Guardian.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Identity.Data;

namespace Guardian.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            
        }

        public async Task Login(LoginRequest model)
        {
            // find user by email in the database
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return;
            }

            // check if provided password matches
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (isPasswordCorrect)
            {
                // Generate an authentication token if the user has been found
                var authenticationToken = GenerateAuthenticationToken();
            }

            

        }

        private string GenerateAuthenticationToken()
        {
            string? secureKey = _jwtSettings.Key;
            return "Not implemented";
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request)
        {
            RegistrationResponse response = new();
            // use the request information to create a new user
            ApplicationUser newUser = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email
            };

            // create a new user on the platform making use of the user manager
            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (result.Succeeded)
            {
                response.UserId = newUser.Id;
                response.IsCreated = true;           
            }
            else
            {
                response.UserId = string.Empty;
                response.IsCreated = false;
                foreach(var error in result.Errors)
                {
                    response.ErrorMessages.Add(error.Code, error.Description);
                }
            }

            return response;
            
        }
    }
}
