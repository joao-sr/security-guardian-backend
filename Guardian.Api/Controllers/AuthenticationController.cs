using Guardian.Application.Contracts;
using Guardian.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Guardian.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: api/<AuthenticationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthenticationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenticationController>
        [HttpPost("register")]
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

        
    }
}
