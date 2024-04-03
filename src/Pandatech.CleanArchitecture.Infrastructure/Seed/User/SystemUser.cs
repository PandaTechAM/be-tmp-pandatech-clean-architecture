﻿using System.Collections;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;
using Pandatech.Crypto;

namespace Pandatech.CleanArchitecture.Infrastructure.Seed.User;

public static class SystemUser
{
   public static WebApplication SeedSystemUser(this WebApplication app)
   {
      using var scope = app.Services.CreateScope();
      var services = scope.ServiceProvider;
      var context = services.GetRequiredService<PostgresContext>();
      var configuration = services.GetRequiredService<IConfiguration>();
      var argon2Id = services.GetRequiredService<Argon2Id>();

      var username = configuration[ConfigurationPaths.SuperUsername];
      ValidateConfiguration(username, ConfigurationPaths.SuperUserPassword);

      var normalizedUsername = username!.ToLowerInvariant();
      var existingUsers = context.Users
         .Where(u => u.Username == normalizedUsername || u.Role == UserRole.SuperAdmin)
         .ToList();

      ValidateSuperUserUniqueness(existingUsers);

      if (existingUsers.Count == 1)
      {
         return app;
      }

      var userPassword = configuration[ConfigurationPaths.SuperUserPassword];
      ValidateConfiguration(userPassword, ConfigurationPaths.SuperUserPassword);

      var passwordHash = argon2Id.HashPassword(userPassword!);

      var newUser = CreateNewUser(normalizedUsername, passwordHash);
      context.Users.Add(newUser);
      context.SaveChanges();

      return app;
   }

   private static void ValidateConfiguration(string? configValue, string configName)
   {
      if (string.IsNullOrWhiteSpace(configValue))
      {
         throw new ArgumentException($"{configName} is not set in appsettings.json");
      }
   }

   private static void ValidateSuperUserUniqueness(ICollection users)
   {
      if (users.Count > 1)
      {
         throw new InvalidOperationException("There are multiple super users in the database.");
      }
   }

   private static UserEntity CreateNewUser(string username, byte[] passwordHash)
   {
      return new UserEntity
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
