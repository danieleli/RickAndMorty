namespace RickAndMorty.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class SwaggerExtensions
    {
        public static void ConfigureSwagger(
            this IServiceCollection services,
            string version,
            string title,
            string? name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = version;
            }

            OpenApiSecurityRequirement security = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Scheme = "Bearer",

                        // The Reference for this Security Requirement refers to the Security Definition of the same name.
                        Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                    },
                    new List<string>()
                }
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name ?? "v1", new OpenApiInfo
                {
                    Version = version,
                    Title = title,
                    Contact = new OpenApiContact
                    {
                        Name = "Jason Sukut",
                        Email = "jason.sukut@gmail.com",
                    },
                });

                c.AddSecurityRequirement(security);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwagger(
            this IApplicationBuilder applicationBuilder,
            string routePrefix,
            string serviceName,
            string schemeAndDomain)
        {
            applicationBuilder.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((document, request) =>
                {
                    // Configure OpenAPI 3.0 to have the correct full route to application.
                    document.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{schemeAndDomain}{routePrefix}" } };
                });
            });

            applicationBuilder.UseSwaggerUI(options =>
            {
                options.DocumentTitle = $"{serviceName}";
                options.SwaggerEndpoint($"{routePrefix}/swagger/v1/swagger.json", "V1");
            });
        }

        public static IEnumerable<T> GetAttributes<T>(this OperationFilterContext context)
            where T : Attribute
        {
            return context.ApiDescription.GetAttributes<T>();
        }

        public static IEnumerable<T> GetAttributes<T>(this ApiDescription apiDescription)
            where T : Attribute
        {
            IEnumerable<T> result = Array.Empty<T>();

            if (apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                && methodInfo.DeclaringType != null)
            {
                result = methodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<T>();
            }

            return result;
        }
    }
}