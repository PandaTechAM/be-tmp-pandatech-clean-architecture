using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Core.Helpers;

namespace Pandatech.CleanArchitecture.Core;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddCoreLayer(this WebApplicationBuilder builder)
   {
      AssemblyRegistry.AddAssemblies(typeof(DependencyInjection).Assembly);
      return builder;
   }
}
