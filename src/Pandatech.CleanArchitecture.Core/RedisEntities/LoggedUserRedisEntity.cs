using Pandatech.CleanArchitecture.Core.Enums;
using ProtoBuf;

namespace Pandatech.CleanArchitecture.Core.RedisEntities;

[ProtoContract]
public class LoggedUserRedisEntity
{
   [ProtoMember(1)] public string AccessToken { get; set; } = null!;
   [ProtoMember(2)] public DateTime AccessTokenExpiration { get; set; }
   [ProtoMember(3)] public long UserId { get; set; }
   [ProtoMember(4)] public string Username { get; set; } = null!;
   [ProtoMember(5)] public string FullName { get; set; } = null!;
   [ProtoMember(6)] public UserRole UserRole { get; set; }
   [ProtoMember(7)] public UserStatus UserStatus { get; set; }
   [ProtoMember(8)] public bool ForcePasswordChange { get; set; }
   [ProtoMember(9)] public DateTime CreatedAt { get; set; }
   [ProtoMember(10)] public DateTime UpdatedAt { get; set; }
}
