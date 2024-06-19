using System.Linq.Expressions;
using GridifyExtensions.Extensions;
using GridifyExtensions.Models;
using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(PostgresContext context) : IBaseRepository<TEntity>
   where TEntity : class
{
   protected readonly PostgresContext Context = context;

   public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
   {
      return await Context.Set<TEntity>().FindAsync([id], cancellationToken);
   }

   public async Task<TEntity?> GetByIdNoTrackingAsync(long id, CancellationToken cancellationToken = default)
   {
      return await Context.Set<TEntity>().AsNoTracking()
         .FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(id), cancellationToken);
   }

   public void Add(TEntity entity)
   {
      Context.Set<TEntity>().Add(entity);
   }

   public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
   {
      await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
   }

   public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
   {
      return await Context.Set<TEntity>().AnyAsync(predicate);
   }

   public void Update(TEntity entity, CancellationToken cancellationToken = default)
   {
      Context.Set<TEntity>().Attach(entity);
      Context.Entry(entity).State = EntityState.Modified;
   }

   public void Remove(TEntity entity, CancellationToken cancellationToken = default)
   {
      Context.Set<TEntity>().Remove(entity);
   }

   public void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
   {
      Context.Set<TEntity>().RemoveRange(entities);
   }

   public Task<PagedResponse<TEntity>> GetPagedAsync(GridifyQueryModel model,
      CancellationToken cancellationToken = default)
   {
      return Context
         .Set<TEntity>()
         .GetPagedAsync(model, cancellationToken);
   }

   public IQueryable<TEntity> ApplyOrder(GridifyQueryModel model)
   {
      return Context
         .Set<TEntity>()
         .ApplyOrder(model);
   }

   public IQueryable<TEntity> ApplyFilter(GridifyQueryModel model)
   {
      return Context
         .Set<TEntity>()
         .ApplyFilter(model);
   }

   public Task<PagedResponse<TDto>> FilterOrderAndGetPagedAsync<TDto>(GridifyQueryModel model,
      Expression<Func<TEntity, TDto>> selectExpression,
      CancellationToken cancellationToken = default)
   {
      return Context
         .Set<TEntity>()
         .FilterOrderAndGetPagedAsync(model, selectExpression, cancellationToken);
   }

   public Task<PagedResponse<TEntity>> FilterOrderAndGetPagedAsync(GridifyQueryModel model,
      CancellationToken cancellationToken = default)
   {
      return Context
         .Set<TEntity>()
         .FilterOrderAndGetPagedAsync(model, cancellationToken);
   }


   public Task<PagedResponse<object>> ColumnDistinctValuesAsync(ColumnDistinctValueQueryModel queryModel,
      CancellationToken cancellationToken = default)
   {
      return Context.Set<TEntity>().ColumnDistinctValuesAsync(queryModel, cancellationToken: cancellationToken);
   }

   public async Task<object?> AggregateAsync(AggregateQueryModel queryModel,
      CancellationToken cancellationToken = default)
   {
      return await Context.Set<TEntity>().AggregateAsync(queryModel, cancellationToken);
   }
}
