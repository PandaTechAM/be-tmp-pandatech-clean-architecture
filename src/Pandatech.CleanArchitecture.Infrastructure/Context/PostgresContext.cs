using EFCore.AuditBase;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using PandaTech.IEnumerableFilters.PostgresContext;

namespace Pandatech.CleanArchitecture.Infrastructure.Context;

//hint for migration: dotnet ef migrations add --project src\Pandatech.CleanArchitecture.Infrastructure\Pandatech.CleanArchitecture.Infrastructure.csproj --context Pandatech.CleanArchitecture.Infrastructure.Context.PostgresContext --configuration Debug --output-dir ./Context/Migrations
public class PostgresContext : PostgresDbContext
{
   public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
   {
      this.UseAuditPropertyValidation();
   }

   public DbSet<UserTokenEntity> UserTokens { get; set; } = null!;
   public DbSet<UserEntity> Users { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);

      modelBuilder.AddTransactionalOutboxEntities();
      modelBuilder.FilterOutDeletedMarkedObjects();
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
   }
}
