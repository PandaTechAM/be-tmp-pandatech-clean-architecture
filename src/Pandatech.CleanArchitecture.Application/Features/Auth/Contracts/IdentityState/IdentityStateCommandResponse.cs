using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.IdentityState;

public class IdentityStateCommandResponse(
   long userId,
   UserStatus status,
   string fullName,
   UserRole userRole)
{
   [PropertyBaseConverter] public long UserId { get; set; } = userId;
   public UserStatus Status { get; set; } = status;
   public string FullName { get; set; } = fullName;
   public UserRole UserRole { get; set; } = userRole;

   public static IdentityStateCommandResponse MapFromIdentity(Identity identity)
   {
      return new IdentityStateCommandResponse(
         identity.UserId,
         identity.Status,
         identity.FullName,
         identity.UserRole);
   }
}
