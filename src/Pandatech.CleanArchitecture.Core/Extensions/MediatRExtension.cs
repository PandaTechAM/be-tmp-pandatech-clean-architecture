using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Core.Behaviors;
using Pandatech.CleanArchitecture.Core.Helpers;

namespace Pandatech.CleanArchitecture.Core.Extensions;

public static class MediatrExtension
{
   public static WebApplicationBuilder AddMediatrWithBehaviors(this WebApplicationBuilder builder)
   {
      var assemblies = AssemblyRegistry.GetAllAssemblies();
      builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies.ToArray()));
      builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorWithoutResponse<,>));
      builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorWithResponse<,>));
      builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
      builder.Services.AddValidatorsFromAssemblies(assemblies);
      return builder;
   }
}
