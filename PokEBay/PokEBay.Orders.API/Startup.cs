using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PokEBay.Orders.API.Infrastructure;
using System;

namespace PokEBay.Orders.API
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
            bool isDatabaseInMemory = Convert.ToBoolean(Configuration["DatabaseConfiguration:InMemory"]);

            if (isDatabaseInMemory)
            {
                services.AddDbContext<OrderContext>(options =>
                                                    options.UseInMemoryDatabase(Configuration["DatabaseConfiguration:Name"]));
            }
            else
            {
                services.AddDbContext<OrderContext>(options =>
                                                    options.UseSqlServer(Configuration["DatabaseConfiguration:ConnectionString"]));
            }

            services.AddScoped<IOrderContext>(
                provider => provider.GetService<OrderContext>());

            services.AddScoped<IOrderService, OrderService>();

            services.AddAutoMapper(typeof(Startup));

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

            //app.UseAuthorization();
            
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
