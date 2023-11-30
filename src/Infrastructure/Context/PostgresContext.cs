using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<TestEntity> Test { get; set; }
}
