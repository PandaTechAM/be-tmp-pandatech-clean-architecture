using EFCore.AuditBase;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Core.Entities;

public class User : AuditEntityBase
{
   public long Id { get; set; }
   public string Username { get; set; } = null!;
   public string FullName { get; set; } = null!;
   public byte[] PasswordHash { get; set; } = null!;
   public UserRole Role { get; set; }
   public UserStatus Status { get; set; } = UserStatus.Active;
   public bool ForcePasswordChange { get; set; } = true;
   public string? Comment { get; set; }

   public ICollection<Token> Tokens { get; set; } = new List<Token>();
}
