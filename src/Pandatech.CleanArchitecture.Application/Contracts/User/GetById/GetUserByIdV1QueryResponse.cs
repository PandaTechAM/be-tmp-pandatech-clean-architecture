using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Contracts.User.GetById;

public class GetUserByIdV1QueryResponse
{
   [PandaPropertyBaseConverter] public long Id { get; set; }
   public string Username { get; set; } = null!;
   public string FullName { get; set; } = null!;
   public UserRole Role { get; set; }
   public UserStatus Status { get; set; }
   public DateTime CreatedAt { get; set; }
   public DateTime UpdatedAt { get; set; }
   public string? Comment { get; set; }

   public static GetUserByIdV1QueryResponse MapFromEntity(UserEntity entity)
   {
      return new GetUserByIdV1QueryResponse
      {
         Id = entity.Id,
         Username = entity.Username,
         FullName = entity.FullName,
         Role = entity.Role,
         Status = entity.Status,
         CreatedAt = entity.CreatedAt,
         UpdatedAt = entity.UpdatedAt,
         Comment = entity.Comment
      };
   }
}
