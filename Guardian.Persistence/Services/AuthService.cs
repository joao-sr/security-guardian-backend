using Guardian.Application.Contracts;
using Guardian.Domain;
using Guardian.Domain.Models;
using Guardian.Domain.Requests;
using Guardian.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Net;
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

        public async Task<LoginResultResponse> Login(LoginRequest model)
        {
            var loginResult = new LoginResultResponse();
            // find user by email in the database
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                loginResult.Message = "User not found";
                loginResult.IsLoginSuccess = false;
                loginResult.StatusCode = (int)HttpStatusCode.NotFound;
                return loginResult;
            }

            // check if provided password matches
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (isPasswordCorrect)
            {
                // Generate an authentication token if the user has been found
                var authenticationToken = GenerateAuthenticationToken(user);
                loginResult.IsLoginSuccess = true;
                loginResult.Token = authenticationToken;
                loginResult.StatusCode = (int)HttpStatusCode.OK;
                return loginResult;

            }
            else
            {
                loginResult.Message = "Password is incorrect";
                loginResult.IsLoginSuccess = false;
                loginResult.StatusCode = (int)HttpStatusCode.Unauthorized;
                return loginResult;
            }

            

        }

        private string GenerateAuthenticationToken(ApplicationUser user)
        {
            string? secretKey = _jwtSettings.Key;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                ([
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings?.Issuer,
                Audience = _jwtSettings?.Audience,
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
            return token;
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
