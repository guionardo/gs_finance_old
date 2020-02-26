using System;
using System.IO;
using Xunit;
using GS_Finance_Server.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests
{
    public class RepositoriesTests
    {
        private IConfiguration configuration;
        private ILogger<RedisKeyValueRepository> logger;
        
        void setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();

            var mock = new Mock<ILogger<KeyValueRepository>>();
            logger = Mock.Of<ILogger<RedisKeyValueRepository>>();
        }

        [Fact]
        public void KeyValue()
        {
            var kvr = new KeyValueRepository();
            Assert.True(kvr.Set("test", "1234"));
            Assert.Equal("1234",kvr.Get("test"));
        }

        [Fact]
        public void RedisKeyValue()
        {
            setup();
            var rkvr = new RedisKeyValueRepository(configuration, logger);
            Assert.True(rkvr.Set("test","1234"));
            Assert.Equal("1234",rkvr.Get("test"));
        }
    }
}