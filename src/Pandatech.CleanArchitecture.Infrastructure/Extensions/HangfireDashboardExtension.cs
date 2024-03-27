using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class HangfireDashboardExtensions
{
   public static WebApplication UseHangfireServer(this WebApplication app)
   {
      var user = app.Configuration[ConfigurationPaths.HangfireUsername];
      var pass = app.Configuration[ConfigurationPaths.HangfirePassword];

      app.UseHangfireDashboard("/hangfire",
         new DashboardOptions
         {
            DashboardTitle = "JobMaster Dashboard",
            Authorization = new[] { new HangfireCustomBasicAuthenticationFilter { User = user, Pass = pass } }
         });
      app.MapHangfireDashboard();

      return app;
   }
}
