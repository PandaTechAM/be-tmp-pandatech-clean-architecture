using System.Text.Json.Serialization;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Update;

public class UpdateUserV1Command : ICommand
{
   [JsonIgnore] public long Id { get; set; }

   public string Username { get; set; } = null!;
   public string FullName { get; set; } = null!;
   public UserRole Role { get; set; }
   public string? Comment { get; set; }
}
