using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NDS_ToDo.Application.Configuration;

namespace NDS_ToDo.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddAuthConfiguration(configuration);

            services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);

            services
                .AddCors(options =>
                {
                    options
                        .AddPolicy("default", policy =>
                        {
                            policy
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });

            services.ResolveDependencies();
        }

        public static void UseApplicationConfiguration(this IApplicationBuilder app)
        {

            app.UseHttpsRedirection();

            app.UseCors("default");

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }
}
