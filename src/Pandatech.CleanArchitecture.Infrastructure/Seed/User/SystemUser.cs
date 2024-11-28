using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
using Pandatech.Crypto.Helpers;

namespace Pandatech.CleanArchitecture.Infrastructure.Seed.User;

public static class SystemUser
{
   public static WebApplication SeedSystemUser(this WebApplication app)
   {
      using var scope = app.Services.CreateScope();
      var services = scope.ServiceProvider;
      var context = services.GetRequiredService<PostgresContext>();
      var configuration = services.GetRequiredService<IConfiguration>();

      var username = configuration.GetSuperUsername();

      var normalizedUsername = username.ToLowerInvariant();

      var existingUsers = context.Users
                                 .Count(u => u.Username == normalizedUsername || u.Role == UserRole.SuperAdmin);

      if (existingUsers >= 1)
      {
         return app;
      }

      var userPassword = configuration.GetSuperuserPassword();

      var passwordHash = Argon2Id.HashPassword(userPassword);

      var newUser = CreateNewUser(normalizedUsername, passwordHash);
      context.Users.Add(newUser);
      context.SaveChanges();

      return app;
   }

   private static Core.Entities.User CreateNewUser(string username, byte[] passwordHash)
   {
      return new Core.Entities.User
      {
         FullName = "System",
         PasswordHash = passwordHash,
         Username = username,
         Role = UserRole.SuperAdmin,
         ForcePasswordChange = false,
         Comment = "Seeded user, please do not delete",
         CreatedByUserId = null
      };
   }
}