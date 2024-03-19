using System.Text.Json.Serialization;
using Pandatech.CleanArchitecture.Core.Interfaces;


namespace Pandatech.CleanArchitecture.Application.Features.User.UpdatePassword
{
   public class UpdateUserPasswordV1Command : ICommand
   {
      [JsonIgnore]
      public long Id { get; set; }

      public string NewPassword { get; set; } = null!;
   }
}
