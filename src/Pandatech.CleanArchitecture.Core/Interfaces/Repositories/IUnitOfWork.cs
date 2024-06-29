using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

public interface IUnitOfWork
{
   public IUserRepository Users { get; set; }
   public ITokenRepository Tokens { get; set; }
   public IUserConfigRepository UserConfigs { get; set; }


   Task BeginTransactionAsync(CancellationToken cancellationToken = default);
   Task CommitAsync(CancellationToken cancellationToken = default);
   Task RollbackAsync(CancellationToken cancellationToken = default);
   Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
