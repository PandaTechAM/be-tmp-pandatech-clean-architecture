using GridifyExtensions.Models;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetColumnDistinctValues;

public class GetUserColumnDistinctValuesQueryHandler(IUnitOfWork unitOfWork)
   : IQueryHandler<GetUserColumnDistinctValuesQuery, CursoredResponse<object>>
{
   public Task<CursoredResponse<object>> Handle(GetUserColumnDistinctValuesQuery request,
      CancellationToken cancellationToken)
   {
      return unitOfWork
         .Users
         .ColumnDistinctValuesAsync(request, cancellationToken);
   }
}
