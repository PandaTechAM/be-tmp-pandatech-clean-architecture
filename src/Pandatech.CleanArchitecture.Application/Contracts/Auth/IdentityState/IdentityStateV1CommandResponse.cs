using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Contracts.Auth.IdentityState;

public class IdentityStateV1CommandResponse(
   long userId,
   UserStatus status,
   string fullName,
   UserRole userRole)
{
   [PandaPropertyBaseConverter] public long UserId { get; set; } = userId;
   public UserStatus Status { get; set; } = status;
   public string FullName { get; set; } = fullName;
   public UserRole UserRole { get; set; } = userRole;

   public static IdentityStateV1CommandResponse MapFromIdentity(Identity identity)
   {
      return new IdentityStateV1CommandResponse(
         identity.UserId,
         identity.Status,
         identity.FullName,
         identity.UserRole);
   }
}
