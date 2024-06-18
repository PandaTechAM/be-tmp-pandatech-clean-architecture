using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddApplicationLayer(this WebApplicationBuilder builder)
   {
      AssemblyRegistry.AddAssemblies(typeof(DependencyInjection).Assembly);
      
      builder.Services.AddScoped<IRequestContext, RequestContext>();


      return builder;
   }
}
