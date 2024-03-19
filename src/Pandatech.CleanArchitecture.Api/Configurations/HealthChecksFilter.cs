using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pandatech.CleanArchitecture.Api.Configurations;

//This class is created because due to some bug /health endpoint is not working in .NET 8. It's included in Microsoft planning.
public class HealthChecksFilter : IDocumentFilter
{
    private const string HealthCheckEndpoint = "/above-board/panda-wellness";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var currentDocumentName = context.DocumentName;

        if (currentDocumentName != "service")
        {
            return;
        }
        var pathItem = new OpenApiPathItem();
        

        var operation = new OpenApiOperation();

        //operation.

        operation.Tags.Add(new OpenApiTag { Name = "AboveBoard" });

        var properties = new Dictionary<string, OpenApiSchema>
        {
            { "status", new OpenApiSchema { Type = "string" } },
            {
                "errors", new OpenApiSchema
                {
                    Items = new OpenApiSchema
                    {
                        Type = "string",
                    }
                }
            }
        };

        var response = new OpenApiResponse();
        response.Content.Add("application / json", new OpenApiMediaType
        {
            Schema = new OpenApiSchema
            {
                Type = "object",

                AdditionalPropertiesAllowed = true,

                Properties = properties,
            }
        });

        operation.Responses.Add("200", response);

        pathItem.AddOperation(OperationType.Get, operation);

        swaggerDoc.Paths.Add(HealthCheckEndpoint, pathItem);
    }
}
