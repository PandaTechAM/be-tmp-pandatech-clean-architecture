using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Repositories;
using Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class RepositoryExtenstion
{
   internal static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
      builder.Services.AddScoped<IUserRepository, UserRepository>();
      builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();

      return builder;
   }
}
