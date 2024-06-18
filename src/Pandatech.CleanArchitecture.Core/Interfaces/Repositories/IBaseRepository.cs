using System.Linq.Expressions;
using GridifyExtensions.Models;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
   Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
   Task<TEntity?> GetByIdNoTrackingAsync(long id, CancellationToken cancellationToken = default);
   
   public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
   void Add(TEntity entity);
   Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
   void Update(TEntity entity, CancellationToken cancellationToken = default);
   void Remove(TEntity entity, CancellationToken cancellationToken = default);

   void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

   Task<PagedResponse<TEntity>> GetPagedAsync(GridifyQueryModel model, CancellationToken cancellationToken = default);

   IQueryable<TEntity> ApplyOrder(GridifyQueryModel model);

   IQueryable<TEntity> ApplyFilter(GridifyQueryModel model);

   Task<PagedResponse<TDto>> FilterOrderAndGetPagedAsync<TDto>(
      GridifyQueryModel model,
      Expression<Func<TEntity, TDto>> selectExpression,
      CancellationToken cancellationToken = default);

   Task<PagedResponse<TEntity>> FilterOrderAndGetPagedAsync(
      GridifyQueryModel model,
      CancellationToken cancellationToken = default);

   Task<PagedResponse<object>> ColumnDistinctValuesAsync(ColumnDistinctValueQueryModel queryModel,
      CancellationToken cancellationToken = default);

   Task<object?> AggregateAsync(AggregateQueryModel queryModel, CancellationToken cancellationToken = default);

   IEnumerable<MappingModel> GetFilters();
}
