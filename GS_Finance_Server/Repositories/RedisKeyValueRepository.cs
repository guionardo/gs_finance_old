using System;
using GS_Finance_Server.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace GS_Finance_Server.Repositories
{
    public class RedisKeyValueRepository : IKeyValueRepository
    {
        private readonly RedisManagerPool _manager;
        private readonly ILogger _logger;

        public RedisKeyValueRepository(IConfiguration configuration, ILogger<RedisKeyValueRepository> logger)
        {
            _logger = logger;
            logger.LogDebug("RedisKeyValueRepository init");
            var cs = configuration.GetConnectionString("redis");
            _manager = new RedisManagerPool(cs);

            using var client = _manager.GetClient();
            if (!client.Ping())
                throw new RedisException("Error on connection to redis");
        }

        private static string GetRedisKey(string key, string context) =>
            (context ?? "_") + ":" + key;

        public bool Set(string key, string value, string context = null, long expiresIn = 0)
        {
            try
            {
                using var client = _manager.GetClient();
                if (expiresIn > 0)
                    client.SetValue(GetRedisKey(key, context), value, TimeSpan.FromSeconds(expiresIn));
                else
                    client.SetValue(GetRedisKey(key, context), value);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on set");
            }

            return false;
        }

        public string Get(string key, string context = null, string defaultValue = null)
        {
            string result = defaultValue;
            try
            {
                using var client = _manager.GetClient();
                result = client.GetValue(GetRedisKey(key, context));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on get");
            }

            return result;
        }

        public void Purge()
        {
            // Automatic purge on redis
        }
    }
}