using Pandatech.CleanArchitecture.Core.EntityFilters;
using Pandatech.CleanArchitecture.Core.Enums;
using PandaTech.IEnumerableFilters.Attributes;

namespace Pandatech.CleanArchitecture.Core.Entities;

[FilterModel(typeof(UserEntityFilter))]
public class UserEntity
{
  public long Id { get; set; }
  public string Username { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public byte[] PasswordHash { get; set; } = null!;
  public UserRole Role { get; set; }
  public UserStatus Status { get; set; } = UserStatus.Active;
  public bool ForcePasswordChange { get; set; } = true;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public string? Comment { get; set; }

  public ICollection<UserTokenEntity> UserTokens { get; set; } = new List<UserTokenEntity>();
}
