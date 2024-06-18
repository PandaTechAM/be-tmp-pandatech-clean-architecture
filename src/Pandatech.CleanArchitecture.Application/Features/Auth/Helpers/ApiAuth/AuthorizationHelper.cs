using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Helpers.ApiAuth;

public static class AuthorizationHelper
{
   public static void Authorize(Identity identity, UserRole minimumRole, bool isForcedToChange)
   {
      if (identity.UserRole > minimumRole)
      {
         throw new ForbiddenException();
      }

      switch (identity.ForcePasswordChange)
      {
         case true when !isForcedToChange:
            throw new ForbiddenException(ErrorMessages.YourPasswordIsExpired);
         case false when isForcedToChange:
            throw new ForbiddenException(ErrorMessages.YourPasswordIsNotExpired);
      }
   }
}
