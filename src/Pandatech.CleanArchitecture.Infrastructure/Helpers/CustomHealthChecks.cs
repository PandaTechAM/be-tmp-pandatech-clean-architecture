using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Pandatech.CleanArchitecture.Infrastructure.Helpers;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")] //todo how to make this global?
public class CustomHealthChecks : IHealthCheck
{
   private const string Endpoint = "/ping";
   private const string ExpectedResponse = "pong";
   private readonly string _baseUrl;

   public CustomHealthChecks(string baseUrl)
   {
      _baseUrl = baseUrl;
   }

   public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
      CancellationToken cancellationToken = new())
   {
      try
      {
         var httpClient = new HttpClient();

         var response = await httpClient.GetAsync(_baseUrl + Endpoint, cancellationToken);

         var content = await response.Content.ReadAsStringAsync(cancellationToken);
         return content == ExpectedResponse
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy();
      }
      catch (Exception e)
      {
         return HealthCheckResult.Unhealthy(exception: e);
      }
   }
}
