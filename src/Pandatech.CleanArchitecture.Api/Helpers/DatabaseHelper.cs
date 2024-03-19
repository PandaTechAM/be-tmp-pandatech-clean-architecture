using Microsoft.EntityFrameworkCore;

namespace Pandatech.CleanArchitecture.Api.Helpers;

public class DatabaseHelper(IServiceProvider serviceProvider)
{
    public string ResetDatabase<T>() where T : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        return "Database reset success!";
    }
}