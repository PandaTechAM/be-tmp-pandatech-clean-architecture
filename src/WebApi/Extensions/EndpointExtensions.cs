using HealthChecks.UI.Client;
using Infrastructure.Context;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PandaVaultClient;
using PandaWebApi.Helpers;

namespace WebApi.Extensions;

public static class EndpointExtensions
{
    public static void MapPandaStandardEndpoints(this WebApplication app)
    {
        app.MapHealthApi()
            .MapDatabaseResetApi()
            .MapPingApi()
            .MapPandaVaultApi(); //optional
    }

    private static WebApplication MapPingApi(this WebApplication app)
    {
        app.MapGet("/ping", () => "pong").WithTags("Above Board");
        return app;
    }

    private static WebApplication MapDatabaseResetApi(this WebApplication app)
    {
        if (app.Environment.IsLocal())
        {
            app.MapGet("/reset-database", (DatabaseHelper helper) => helper.ResetDatabase<PostgresContext>())
                .WithTags("Above Board");
        }

        return app;
    }

    private static WebApplication MapHealthApi(this WebApplication app)
    {
        app.MapHealthChecks("/panda-wellness", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).WithTags("Above Board");
        return app;
    }
}