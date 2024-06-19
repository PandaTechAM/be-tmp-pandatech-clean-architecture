using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.User.Contracts.GetById;

public class GetUserQueryResponse
{
   [PropertyBaseConverter] public long Id { get; set; }
   public string Username { get; set; } = null!;
   public string FullName { get; set; } = null!;
   public UserRole Role { get; set; }
   public UserStatus Status { get; set; }
   public DateTime CreatedAt { get; set; }
   public DateTime? UpdatedAt { get; set; }
   public string? Comment { get; set; }

   public static GetUserQueryResponse MapFromEntity(Core.Entities.User entity)
   {
      return new GetUserQueryResponse
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
