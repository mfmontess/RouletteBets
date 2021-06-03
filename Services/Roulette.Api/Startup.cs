using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RouletteBets.Api.Repositories;
using Microsoft.Extensions.Options;
using RouletteBets.Api.Models;
namespace RouletteBets.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<Connection>(Configuration.GetSection("Connections"));
            services.AddSingleton<IConnection>(d=> d.GetRequiredService<IOptions<Connection>>().Value);
            services.AddScoped<IRouletteRepository, RouletteRepository>();
            services.AddScoped<IBetRepository, BetRepository>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
