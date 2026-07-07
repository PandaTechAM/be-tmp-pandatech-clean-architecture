using GridifyExtensions.Models;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetColumnDistinctValues;

public class GetUserColumnDistinctValuesQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetUserColumnDistinctValuesQuery, CursoredResponse<object?>>
{
    public Task<CursoredResponse<object?>> Handle(GetUserColumnDistinctValuesQuery request,
        CancellationToken cancellationToken)
    {
        return unitOfWork
            .Users
            .ColumnDistinctValues(request, cancellationToken);
    }
}
