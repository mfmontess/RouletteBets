using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roulette.Api.Repositories;
using Microsoft.Extensions.Options;

namespace Roulette.Api
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
            services.Configure<Roulette.Api.Models.Connection>(Configuration.GetSection("Connections"));
            services.AddSingleton<Roulette.Api.Models.IConnection>(d=> d.GetRequiredService<IOptions<Roulette.Api.Models.Connection>>().Value);
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
