using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PokEBay.Notifications.API.Infrastructure;
using PokEBay.Notifications.API.Infrastructure.EmailService;

namespace PokEBay.Notifications.API
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
            services.AddControllers().AddDapr();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["SwaggerConfiguration:ApiVersion"],
                                new OpenApiInfo
                                {
                                    Title = Configuration["SwaggerConfiguration:ApiName"],
                                    Version = Configuration["SwaggerConfiguration:ApiVersion"]
                                });
            });

            services.AddLogging(loggingBuilder => { loggingBuilder.AddSeq(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                                            Configuration["SwaggerConfiguration:ApiEndpoint"],
                                            $"{Configuration["SwaggerConfiguration:ApiName"]} {Configuration["SwaggerConfiguration:ApiVersion"]}"
                                            ));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
