using GridifyExtensions.Extensions;
using GridifyExtensions.Models;
using Pandatech.CleanArchitecture.Application.Features.User.Contracts.GetById;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetUsers;

public class GetUsersQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetUsersQuery, PagedResponse<GetUserQueryResponse>>
{
    public Task<PagedResponse<GetUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return unitOfWork
            .Users
            .WhereNotSuperAdmin()
            .FilterOrderAndGetPagedAsync(request,
                x => new GetUserQueryResponse
                {
                    Id = x.Id,
                    Username = x.Username,
                    FullName = x.FullName,
                    Role = x.Role,
                    Status = x.Status,
                    Comment = x.Comment
                },
                cancellationToken);
    }
}
