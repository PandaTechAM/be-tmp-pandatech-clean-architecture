namespace Pandatech.CleanArchitecture.Api.Helpers;

public static class ApiHelper
{
   private const string BaseApiPath = "/api/v";

   public const string GroupNameClean = "Clean";


   public static string GetRoutePrefix(int version, string baseRoute)
   {
      return $"{BaseApiPath}{version}{baseRoute}";
   }
}
