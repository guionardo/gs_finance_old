using GS_Finance_Server.Interfaces;
using GS_Finance_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace GS_Finance_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILoginRequestService _loginRequestService;

        public AuthenticationController(ILoginRequestService loginRequestService)
        {
            _loginRequestService = loginRequestService;
        }
        

        [HttpGet("login")]
        public AuthenticationData Login(string username, string password)
        {
            if (!_loginRequestService.ValidateLogin(username, password))
                return new AuthenticationData(
                    "",
                    "LOGIN ERROR",
                    0);
            
            return new AuthenticationData(
                _loginRequestService.AuthenticationToken(username),
                "LOGIN OK",
                _loginRequestService.NextValidUntilTimestamp());
            
        }
    }
}