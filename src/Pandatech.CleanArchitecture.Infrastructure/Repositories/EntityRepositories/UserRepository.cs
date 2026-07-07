using EFCore.AuditBase;
using GridifyExtensions.Extensions;
using GridifyExtensions.Models;
using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class UserRepository(PostgresContext postgresContext)
    : BaseRepository<User>(postgresContext), IUserRepository
{
    public Task<bool> IsUsernameDuplicate(string username, CancellationToken cancellationToken = default)
    {
        return Context.Users.AnyAsync(x => x.Username == username, cancellationToken);
    }

    public Task<List<User>> GetByIdsExceptSuper(List<long> ids, CancellationToken cancellationToken = default)
    {
        return Context.Users
            .Where(x => ids.Contains(x.Id))
            .Where(x => x.Role != UserRole.SuperAdmin)
            .ToListAsync(cancellationToken);
    }

    public IQueryable<User> WhereNotSuperAdmin()
    {
        return Context.Users
            .Where(u => u.Role != UserRole.SuperAdmin);
    }

    public Task<User?> GetByUsername(string username, CancellationToken cancellationToken = default)
    {
        return Context.Users
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }

    public Task Delete(string requestFilter, long identityUserId, CancellationToken cancellationToken)
    {
        var filterModel = new GridifyQueryModel
        {
            Page = 1,
            PageSize = 1,
            OrderBy = null,
            Filter = requestFilter
        };

        return Context.Users
            .Where(x => x.Role != UserRole.SuperAdmin)
            .ApplyFilter(filterModel)
            .ExecuteSoftDeleteAsync(identityUserId, ct: cancellationToken);
    }
}
