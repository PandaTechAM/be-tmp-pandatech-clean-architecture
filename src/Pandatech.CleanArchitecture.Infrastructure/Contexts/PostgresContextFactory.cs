using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pandatech.CleanArchitecture.Infrastructure.Contexts;

namespace Pandatech.CleanArchitecture.Infrastructure.Context;

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
{
   public PostgresContext CreateDbContext(string[] args)
   {
      var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();

      optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

      return new PostgresContext(optionsBuilder.Options);
   }
}
