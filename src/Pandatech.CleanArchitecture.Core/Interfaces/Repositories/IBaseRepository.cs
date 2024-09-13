using System.Linq.Expressions;
using GridifyExtensions.Models;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
   Task<TEntity?> GetById(long id, CancellationToken cancellationToken = default);
   Task<TEntity?> GetByIdNoTracking(long id, CancellationToken cancellationToken = default);

   public Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
   void Add(TEntity entity);
   Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
   void Update(TEntity entity, CancellationToken cancellationToken = default);
   void Remove(TEntity entity, CancellationToken cancellationToken = default);

   void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

   Task<PagedResponse<TEntity>> GetPaged(GridifyQueryModel model, CancellationToken cancellationToken = default);

   IQueryable<TEntity> ApplyOrder(GridifyQueryModel model);

   IQueryable<TEntity> ApplyFilter(GridifyQueryModel model);

   Task<PagedResponse<TDto>> FilterOrderAndGetPaged<TDto>(GridifyQueryModel model,
      Expression<Func<TEntity, TDto>> selectExpression,
      CancellationToken cancellationToken = default);

   Task<PagedResponse<TEntity>> FilterOrderAndGetPaged(GridifyQueryModel model,
      CancellationToken cancellationToken = default);

   Task<CursoredResponse<object>> ColumnDistinctValues(ColumnDistinctValueCursoredQueryModel queryModel,
      CancellationToken cancellationToken = default);

   Task<object?> Aggregate(AggregateQueryModel queryModel, CancellationToken cancellationToken = default);
}