using Microsoft.OpenApi.Models;

namespace IFCE.NDS.ToDoList.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            var contact = new OpenApiContact
            {
                Name = "CTI IFCE Maracanaú",
                Email = "cti@maracanau.ifce.edu.br",
                Url = new Uri("https://ifce.edu.br/maracanau")
            };

            var license = new OpenApiLicense
            {
                Name = "Free License",
                Url = new Uri("https://ifce.edu.br/maracanau")
            };

            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(o =>
                {
                    o.EnableAnnotations();
                    o.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Todo List API",
                        Contact = contact,
                        License = license
                    });
                    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JSON Web Token based security",
                    });
                    o.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                    });
                });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app
                .UseSwagger()
                .UseSwaggerUI();
            //}
        }

    }
}
