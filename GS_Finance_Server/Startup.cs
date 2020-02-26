using GS_Finance_Server.Interfaces;
using GS_Finance_Server.Interfaces.Repositories;
using GS_Finance_Server.Models;
using GS_Finance_Server.Repositories;
using GS_Finance_Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GS_Finance_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddSingleton<ILoginRequestService, LoginRequestService>();
            services.AddSingleton<IRepository<User>, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IKeyValueRepository, KeyValueRepository>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}