using Microsoft.EntityFrameworkCore.Storage;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories;

public class UnitOfWork(
   IUserRepository userRepository,
   ITokenRepository tokenRepository,
   IUserConfigRepository userConfigRepository,
   PostgresContext context)
   : IUnitOfWork
{
   private IDbContextTransaction _transaction = null!;
   public IUserRepository Users { get; set; } = userRepository;
   public ITokenRepository Tokens { get; set; } = tokenRepository;

   public IUserConfigRepository UserConfigs { get; set; } = userConfigRepository;


   public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
   {
      if (_transaction != null)
      {
         throw new InvalidOperationException("A transaction is already in progress.");
      }

      _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
   }

   public async Task CommitAsync(CancellationToken cancellationToken = default)
   {
      try
      {
         await SaveChangesAsync(cancellationToken);
         await _transaction.CommitAsync(cancellationToken);
      }
      finally
      {
         await _transaction.DisposeAsync();
         _transaction = null!;
      }
   }

   public async Task RollbackAsync(CancellationToken cancellationToken = default)
   {
      try
      {
         await _transaction.RollbackAsync(cancellationToken);
      }
      finally
      {
         await _transaction.DisposeAsync();
         _transaction = null!;
      }
   }

   public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
   {
      await context.SaveChangesAsync(cancellationToken);
   }
}
