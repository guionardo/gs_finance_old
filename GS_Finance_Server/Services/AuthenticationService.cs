using System;
using System.Security.Authentication;
using GS_Finance_Server.Exceptions;
using GS_Finance_Server.Interfaces;
using GS_Finance_Server.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace GS_Finance_Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IKeyValueRepository _repository;
        private long _tokenMaximumAge;
        private const string BaseKey = "Auth";

        public AuthenticationService(IKeyValueRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _tokenMaximumAge = configuration.GetValue<long>("TokenMaximumAge", 60 * 60 * 24);
        }

        public string GetAuthenticationToken(string key)
        {
            var token = Guid.NewGuid().ToString();
            if (_repository.Set(key, token, BaseKey, _tokenMaximumAge))
                return token;

            throw new AuthenticationServiceException();
        }

        public bool ValidateToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}