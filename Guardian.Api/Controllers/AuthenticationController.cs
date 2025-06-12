using Guardian.Application.Contracts;
using Guardian.Domain.Requests;
using Guardian.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Guardian.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [HttpPost("api/register")]
        public async Task<ActionResult<RegistrationResponse>> CreateUserAsync(RegistrationRequest request)
        {
            RegistrationResponse result = await _authService.RegisterUserAsync(request);
            if (result.IsCreated)
            {
                return Ok(result);
            }else if (!result.IsCreated)
            {
                return BadRequest(result);
            }

            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await _authService.Login(model);
            return Ok(result);
        }

        
    }
}
