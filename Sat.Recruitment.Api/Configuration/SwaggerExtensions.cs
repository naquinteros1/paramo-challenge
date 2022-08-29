using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
namespace Sat.Recruitment.Api.Configuration
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                // Información general

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = configuration.GetSection("Swagger:Title").Value,
                    Description = configuration.GetSection("Swagger:Description").Value,
                    Contact = new OpenApiContact
                    {
                        Name = configuration.GetSection("Swagger:Contact:Name").Value,
                        Email = configuration.GetSection("Swagger:Contact:Email").Value,
                        Url = new Uri(configuration.GetSection("Swagger:Contact:Url").Value),
                    }
                });

                // Servidores de prueba

                var servers = configuration.GetSection("Swagger:Servers").Get<List<OpenApiServer>>();
                if (servers != null)
                {
                    foreach (var srv in servers)
                        options.AddServer(srv);
                }

                // Importación de comentarios / documentación de clases

                var xmlControllers = Path.Combine(AppContext.BaseDirectory, "Sat.Recruitment.Api.xml");
                var xmlModels = Path.Combine(AppContext.BaseDirectory, "Sat.Domain.xml");

                options.IncludeXmlComments(xmlControllers);
                options.IncludeXmlComments(xmlModels);

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs";

                options.InjectStylesheet("../css/swagger-ui/theme-flattop.css");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", configuration.GetSection("Swagger:Title").Value);
            });

            return app;
        }
    }
}
