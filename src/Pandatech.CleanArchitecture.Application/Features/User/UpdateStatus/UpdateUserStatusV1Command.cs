using System.Text.Json.Serialization;
using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.UpdateStatus;

public class UpdateUserStatusV1Command : ICommand
{
   [PandaPropertyBaseConverter]
   [JsonIgnore]
   public long Id { get; set; }

   public UserStatus Status { get; set; }
}
