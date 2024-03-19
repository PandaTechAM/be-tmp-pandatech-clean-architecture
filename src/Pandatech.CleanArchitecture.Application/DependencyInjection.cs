using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Pandatech.CleanArchitecture.Application;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddApplicationLayer(this WebApplicationBuilder builder)
   {
      var assembly = typeof(DependencyInjection).Assembly;
      builder.AddMediatrWithValidator(assembly);


      return builder;
   }

   private static WebApplicationBuilder AddMediatrWithValidator(this WebApplicationBuilder builder, Assembly assembly)
   {
      builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
      builder.Services.AddValidatorsFromAssembly(assembly);
      return builder;
   }
}
