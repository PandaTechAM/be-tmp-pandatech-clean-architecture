using GridifyExtensions.Models;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetColumnDistinctValues;

public class GetUserColumnDistinctValuesQuery : ColumnDistinctValueCursoredQueryModel, IQuery<CursoredResponse<object>>;