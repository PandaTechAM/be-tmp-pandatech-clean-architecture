using Core.Entities;
using Core.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

internal class TestRepository : BaseRepository<TestEntity>, ITestRepository
{
    public TestRepository(PostgresContext postgresContext) : base(postgresContext)
    {
    }
}