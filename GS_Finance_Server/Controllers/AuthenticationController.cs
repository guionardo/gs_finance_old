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
            string token = "";
            string msg = "LOGIN ERROR";
            long validUntil = 0;
            if (_loginRequestService.ValidateLogin(username, password))
            {
                token = _loginRequestService.AuthenticationToken(username);
                msg = "LOGIN OK";
                validUntil = _loginRequestService.NextValidUntilTimestamp();
            }

            return new AuthenticationData(
                token,
                msg,
                validUntil);
        }
    }
}