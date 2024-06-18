using System.Text.Json.Serialization;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Update;

public class UpdateUserCommand : ICommand
{
   [JsonIgnore] public long Id { get; set; }

   public string Username { get; set; } = null!;
   public string FullName { get; set; } = null!;
   public UserRole Role { get; set; }
   public string? Comment { get; set; }
}
