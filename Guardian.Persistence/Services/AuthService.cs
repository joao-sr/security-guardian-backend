using Guardian.Application.Contracts;
using Guardian.Domain;
using Guardian.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
