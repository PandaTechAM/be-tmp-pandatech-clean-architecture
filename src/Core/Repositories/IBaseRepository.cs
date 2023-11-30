namespace Core.Repositories;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity product);
    void Update(TEntity product);
    Task DeleteAsync(int id);
}