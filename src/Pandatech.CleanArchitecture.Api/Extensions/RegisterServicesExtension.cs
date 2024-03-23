using System.Reflection;
using BaseConverter;
using FluentValidation;
using Pandatech.CleanArchitecture.Core.Extensions;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;
using Pandatech.Crypto;
using PandaTech.IEnumerableFilters.Extensions;

namespace Pandatech.CleanArchitecture.Api.Extensions;

public static class RegisterServicesExtension
{
   public static WebApplicationBuilder RegisterAllServices(this WebApplicationBuilder builder)
   {
      builder.AddPandaStandardServices();

      return builder;
   }


   private static WebApplicationBuilder AddPandaStandardServices(this WebApplicationBuilder builder)
   {
      if (builder.Environment.IsLocal())
      {
         builder.Services.AddSingleton<DatabaseHelper>();
      }

      
      return builder;
   }
}
