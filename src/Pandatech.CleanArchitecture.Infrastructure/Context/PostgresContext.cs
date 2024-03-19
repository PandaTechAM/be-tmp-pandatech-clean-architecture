using Hangfire.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pandatech.CleanArchitecture.Core.Entities;
using PandaTech.IEnumerableFilters.PostgresContext;

namespace Pandatech.CleanArchitecture.Infrastructure.Context;

//hint for migration: dotnet ef migrations add --project src\Pandatech.CleanArchitecture.Infrastructure\Pandatech.CleanArchitecture.Infrastructure.csproj --output-dir ․/Migrations
public class PostgresContext(DbContextOptions<PostgresContext> options) : PostgresDbContext(options)
{
   public DbSet<UserTokenEntity> UserTokens { get; set; } = null!;
   public DbSet<UserEntity> Users { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);

      modelBuilder.OnHangfireModelCreating();
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
   }
}

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
{
   public PostgresContext CreateDbContext(string[] args)
   {
      var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();

      optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

      return new PostgresContext(optionsBuilder.Options);
   }
}
