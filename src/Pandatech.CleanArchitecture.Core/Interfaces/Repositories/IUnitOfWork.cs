using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

public interface IUnitOfWork
{
    public IUserRepository Users { get; set; }
    public ITokenRepository Tokens { get; set; }
    public IUserConfigRepository UserConfigs { get; set; }


    Task BeginTransaction(CancellationToken cancellationToken = default);
    Task Commit(CancellationToken cancellationToken = default);
    Task Rollback(CancellationToken cancellationToken = default);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
