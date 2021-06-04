using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RouletteBets.Api.Repositories;
using Microsoft.Extensions.Options;
using RouletteBets.Api.Models;
using Microsoft.OpenApi.Models;
using System;
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
            AddSwagger(services);
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"RouletteBets {groupName}",
                    Version = groupName,
                    Description = "RouletteBets API",
                    Contact = new OpenApiContact
                    {
                        Name = "Michael Montes",
                        Email = "mfmontess@outlook.com",
                        Url = new Uri("https://www.linkedin.com/in/mfmontess/"),
                    }
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RouletteBets API V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
