using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;
using PandaTech.IEnumerableFilters.Attributes;
using PandaTech.IEnumerableFilters.Converters;

namespace Pandatech.CleanArchitecture.Core.EntityFilters;

public abstract class UserEntityFilter
{
   [MappedToProperty(nameof(UserEntity.Id), ConverterType = typeof(FilterPandaBaseConverter))]
   public long Id { get; set; }

   [MappedToProperty(nameof(UserEntity.Username))]
   [Order]
   public string Username { get; set; } = null!;

   [MappedToProperty(nameof(UserEntity.FullName))]
   public string FullName { get; set; } = null!;

   [MappedToProperty(nameof(UserEntity.CreatedAt))]
   public DateTime CreationDate { get; set; }

   [MappedToProperty(nameof(UserEntity.Status))]
   public UserStatus Status { get; set; }

   [MappedToProperty(nameof(UserEntity.Comment))]
   public string? Comment { get; set; }
}
