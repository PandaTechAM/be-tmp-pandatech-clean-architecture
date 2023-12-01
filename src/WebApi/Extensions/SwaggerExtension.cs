using BaseConverter;
using Microsoft.OpenApi.Models;
using WebApi.Configurations;

namespace WebApi.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection
        AddPandaSwaggerGen(this IServiceCollection services, IConfiguration configuration) // Move to common
    {
        services.AddSwaggerGen(options =>
        {
            var swaggerOptions =
                configuration.GetSection("Swagger").Get<SwaggerOptions>() ?? SwaggerOptions.Default;

            foreach (var version in swaggerOptions.Versions)
            {
                options.SwaggerDoc(version.Key, new OpenApiInfo
                {
                    Title = version.Title,
                    Description = version.Description,
                    //  "Powered by PandaTech LLC: Where precision meets innovation. Let's build the future, one endpoint at a time.",
                    Contact = new OpenApiContact
                    {
                        Name = "PandaTech LLC",
                        Email = "info@pandatech.it",
                        Url = new Uri("https://pandatech.it"),
                    }
                });
            }

            //This option is created because due to some bug /health endpoint is not working in .NET 7. It's included in Microsoft planning.
            options.DocumentFilter<HealthChecksFilter>();

            // Add string input support into int64 field
            options.ParameterFilter<PandaParameterBaseConverterAttribute>();

            // Add the token authentication option
            options.AddSecurityDefinition("token", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "token",
                Description = "Token authentication using the bearer scheme"
            });

            // Require the token to be passed as a header for API calls
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static void UsePandaSwagger(this WebApplication app, IConfiguration configuration)
    {
        if (app.Environment.IsProduction()) return;

        var swaggerOptions =
            configuration.GetSection("Swagger").Get<SwaggerOptions>() ?? SwaggerOptions.Default;

        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Url =
                            $"https://{httpReq.Host.Value}{swaggerOptions.ApiBasePath}"
                    }
                };
            });
        });
        app.UseSwaggerUI(c =>
        {
            foreach (var version in swaggerOptions.Versions)
            {
                c.SwaggerEndpoint($"{swaggerOptions.JsonRoutePrefix}/swagger/{version.Key}/swagger.json",
                    version.Title);
            }

            c.RoutePrefix = "swagger";
        });

        app.UseSwaggerUI(options =>
        {
            options.InjectStylesheet("/assets/css/panda-style.css");
            options.InjectJavascript("/assets/js/docs.js");
            options.DocExpansion(DocExpansion.None);
        });
    } // move to common

    public class SwaggerOptions
    {
        public static SwaggerOptions Default = new()
        {
            Enabled = false,
            JsonRoutePrefix = "",
            ApiBasePath = "/",
            ApiBaseScheme = "http",
            Versions = new List<SwaggerVersionOptions>
            {
                new()
                {
                    Key = "v1",
                    Title = "API V1",
                    Description = "API V1"
                }
            }
        };

        public bool Enabled { get; set; }

        public string JsonRoutePrefix { get; set; } = null!;

        public string ApiBasePath { get; set; } = null!;

        public string ApiBaseScheme { get; set; } = null!;

        public List<SwaggerVersionOptions> Versions { get; set; } = null!;
    } // move to common

    public class SwaggerVersionOptions
    {
        public string Key { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    } // move to common
}
