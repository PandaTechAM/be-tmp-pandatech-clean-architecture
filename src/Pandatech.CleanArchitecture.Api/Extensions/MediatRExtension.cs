using FluentValidation;
using MediatR;
using Pandatech.CleanArchitecture.Core.Behaviors;

namespace Pandatech.CleanArchitecture.Api.Extensions;

public static class MediatrExtension
{
  public static WebApplicationBuilder AddMediatrWithBehaviors(this WebApplicationBuilder builder)
  {
    var assembly = typeof(Program).Assembly;
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorWithoutResponse<,>));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorWithResponse<,>));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddValidatorsFromAssembly(assembly);
    return builder;
  }
}
