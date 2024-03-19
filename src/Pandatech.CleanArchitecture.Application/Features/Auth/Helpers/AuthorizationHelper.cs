using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;

public static class AuthorizationHelper
{
   public static void Authorize(Identity identity, UserRole minimumRole, bool isForcedToChange)
   {
      if (identity.UserRole > minimumRole)
      {
         throw new ForbiddenException("you_are_not_authorized");
      }

      switch (identity.ForcePasswordChange)
      {
         case true when !isForcedToChange:
            throw new ForbiddenException("you_need_to_change_your_password");
         case false when isForcedToChange:
            throw new ForbiddenException("your_password_is_not_expired");
      }
   }
}
