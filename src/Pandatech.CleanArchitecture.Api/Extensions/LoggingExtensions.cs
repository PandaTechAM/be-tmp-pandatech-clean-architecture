using Pandatech.CleanArchitecture.Api.Middlewares;

namespace Pandatech.CleanArchitecture.Api.Extensions;

public static class LoggingExtensions
{
   public static WebApplication UseRequestResponseLogging(this WebApplication app)
   {
      if (app.Logger.IsEnabled(LogLevel.Information))
      {
         app.UseMiddleware<RequestResponseLoggingMiddleware>();
      }

      return app;
   }
}