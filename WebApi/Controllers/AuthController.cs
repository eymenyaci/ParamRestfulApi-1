using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public string Get(string userName,string password)
        {
            string token = _identityService.Login(userName,password);
            return token;
        }
        [HttpGet("ValidateToken")]
        public bool ValidateToken(string token) 
        {
            return _identityService.ValidateToken(token);
        }

    }
}
