using Microsoft.AspNetCore.Mvc;
using Pandatech.CleanArchitecture.Api.Helpers;
using Pandatech.CleanArchitecture.Core.Extensions;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using PandaVaultClient;

namespace Pandatech.CleanArchitecture.Api.Endpoints.SharedEndpoints;

public static class OptionalEndpoints
{
   private const string TagName = "above-board";

   public static void MapPandaOptionalEndpoints(this WebApplication app)
   {
      if (!app.Environment.IsProduction())
      {
         app.MapPandaVaultApi($"/{TagName}/configuration", TagName, ApiHelper.GroupNameMain);
      }

      if (app.Environment.IsLocal())
      {
         app.MapDatabaseResetApi();
      }
   }

   private static WebApplication MapDatabaseResetApi(this WebApplication app)
   {
      app.MapGet("/above-board/reset-database",
            ([FromServices] DatabaseHelper helper) => helper.ResetDatabase<PostgresContext>())
         .WithTags("above-board")
         .WithGroupName(ApiHelper.GroupNameMain);


      return app;
   }
}
