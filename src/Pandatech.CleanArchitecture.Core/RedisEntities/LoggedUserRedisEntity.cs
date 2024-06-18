using DistributedCache.Services.Interfaces;
using MessagePack;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Core.RedisEntities;

[MessagePackObject]
public class LoggedUserRedisEntity : ICacheEntity
{
   [Key(0)] public string AccessToken { get; set; } = null!;
   [Key(1)] public DateTime UpdatedAt { get; set; }
   [Key(2)] public DateTime AccessTokenExpiration { get; set; }
   [Key(3)] public long UserId { get; set; }
   [Key(4)] public string Username { get; set; } = null!;
   [Key(5)] public string FullName { get; set; } = null!;
   [Key(6)] public UserRole UserRole { get; set; }
   [Key(7)] public UserStatus UserStatus { get; set; }
   [Key(8)] public bool ForcePasswordChange { get; set; }
   [Key(9)] public DateTime CreatedAt { get; set; }
}
