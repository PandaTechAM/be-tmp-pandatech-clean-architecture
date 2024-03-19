using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pandatech.CleanArchitecture.Api.Helpers;

namespace Pandatech.CleanArchitecture.Api.Endpoints.SharedEndpoints;

public class SharedEndpoints : ICarterModule
{
   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app.MapGroup("/above-board")
         .WithTags("above-board")
         .WithGroupName(ApiHelper.GroupNameMain)
         .WithOpenApi();

      groupApp.MapGet("/ping", () => "pong").Produces<string>();

      // Check filters in HealthChecksFilter.cs
      groupApp.MapHealthChecks("/panda-wellness",
         new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
   }
}
