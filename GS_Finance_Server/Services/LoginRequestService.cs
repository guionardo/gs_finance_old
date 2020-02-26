using System;
using GS_Finance_Server.Interfaces;

namespace GS_Finance_Server.Services
{
    public class LoginRequestService : ILoginRequestService
    {
        private IUserService _userService;
        private IAuthenticationService _authService;

        public LoginRequestService(IUserService service, IAuthenticationService auth_service)
        {
            _userService = service;
            _authService = auth_service;
        }

        public bool ValidateLogin(string username, string password)
        {
            try
            {
                var user = _userService.Find(username);
                return _userService.ValidatePassword(user, password);
            }
            catch
            {
                // Not found
            }

            return false;
        }

        public long NextValidUntilTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + (24 * 60 * 60);
        }

        public string AuthenticationToken(string username)
        {
            return _authService.GetAuthenticationToken(username);
        }
    }
}