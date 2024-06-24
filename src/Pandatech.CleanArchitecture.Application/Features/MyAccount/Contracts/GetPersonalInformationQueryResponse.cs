using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Contracts;

public record GetPersonalInformationQueryResponse(
   string Username,
   string FullName,
   UserRole UserRole,
   DateTime CreatedAt)
{
   public static GetPersonalInformationQueryResponse MapFromRequestContext(IRequestContext requestContext) =>
      new(requestContext.Identity.Username, requestContext.Identity.FullName, requestContext.Identity.UserRole,
         requestContext.Identity.CreatedAt);
}
