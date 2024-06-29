using EFCore.AuditBase;
using GridifyExtensions.DbContextFunction;
using MassTransit.PostgresOutbox.Abstractions;
using MassTransit.PostgresOutbox.Entities;
using MassTransit.PostgresOutbox.Extensions;
using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Infrastructure.Context;

//hint for migration: dotnet ef migrations add --project src\Pandatech.CleanArchitecture.Infrastructure\Pandatech.CleanArchitecture.Infrastructure.csproj --context Pandatech.CleanArchitecture.Infrastructure.Context.PostgresContext --configuration Debug --output-dir ./Context/Migrations
public class PostgresContext : PostgresFunctions, IOutboxDbContext, IInboxDbContext
{
   public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
   {
      this.UseAuditPropertyValidation();
   }

   public DbSet<Token> Tokens { get; set; } = null!;
   public DbSet<User> Users { get; set; } = null!;
   public DbSet<UserConfig> UserConfigs { get; set; } = null!;
   public DbSet<InboxMessage> InboxMessages { get; set; }

   public DbSet<OutboxMessage> OutboxMessages { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ConfigureInboxOutboxEntities();
      modelBuilder.FilterOutDeletedMarkedObjects();
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
   }
}
