using System.Linq.Expressions;
using PandaTech.IEnumerableFilters.Dto;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
   Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
   Task<TEntity?> GetByIdNoTrackingAsync(long id, CancellationToken cancellationToken = default);
   IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default);
   IQueryable<TEntity> GetAllNoTracking(CancellationToken cancellationToken = default);
   IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

   IQueryable<TEntity> FindNoTracking(Expression<Func<TEntity, bool>> predicate,
      CancellationToken cancellationToken = default);

   public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
   Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
   Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
   void Update(TEntity entity, CancellationToken cancellationToken = default);
   void Remove(TEntity entity, CancellationToken cancellationToken = default);

   void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

   Task<DistinctColumnValuesResult> GetColumnValuesAsync(string columnName, int page, int pageSize,
      string dataRequest);

   Task<List<FilterInfo>> GetFiltersAsync();
}
