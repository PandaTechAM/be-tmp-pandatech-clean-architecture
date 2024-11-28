using GridifyExtensions.Models;
using Pandatech.CleanArchitecture.Application.Features.User.Contracts.GetById;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetUsers;

public class GetUsersQuery : GridifyQueryModel, IQuery<PagedResponse<GetUserQueryResponse>>;