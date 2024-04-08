using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Core.Helpers;

namespace Pandatech.CleanArchitecture.Application;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddApplicationLayer(this WebApplicationBuilder builder)
   {
      AssemblyRegistry.AddAssemblies(typeof(DependencyInjection).Assembly);


      return builder;
   }
}
