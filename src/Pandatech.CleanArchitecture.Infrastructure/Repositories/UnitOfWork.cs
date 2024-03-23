using Microsoft.EntityFrameworkCore.Storage;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories;

public class UnitOfWork(
   IUserRepository userRepository,
   IUserTokenRepository userTokenRepository,
   PostgresContext context,
   IDbContextTransaction transaction)
   : IUnitOfWork
{
   public IUserRepository Users { get; set; } = userRepository;
   public IUserTokenRepository UserTokens { get; set; } = userTokenRepository;

   public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
   {
      if (transaction != null)
      {
         throw new InvalidOperationException("A transaction is already in progress.");
      }

      transaction = await context.Database.BeginTransactionAsync(cancellationToken);
   }

   public async Task CommitAsync(CancellationToken cancellationToken = default)
   {
      try
      {
         await SaveChangesAsync(cancellationToken);
         await transaction.CommitAsync(cancellationToken);
      }
      finally
      {
         await transaction.DisposeAsync();
         transaction = null!;
      }
   }

   public async Task RollbackAsync(CancellationToken cancellationToken = default)
   {
      try
      {
         await transaction.RollbackAsync(cancellationToken);
      }
      finally
      {
         await transaction.DisposeAsync();
         transaction = null!;
      }
   }

   public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
   {
      await context.SaveChangesAsync(cancellationToken);
   }
}
