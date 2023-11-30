using Core.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    // ReSharper disable once InconsistentNaming
    protected readonly PostgresContext _postgresContext;

    public BaseRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _postgresContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _postgresContext.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _postgresContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _postgresContext.Set<TEntity>().Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        TEntity entityToDelete = await _postgresContext.Set<TEntity>().FindAsync(id);
        if (entityToDelete == null) return;

        _postgresContext.Set<TEntity>().Remove(entityToDelete);
    }
}