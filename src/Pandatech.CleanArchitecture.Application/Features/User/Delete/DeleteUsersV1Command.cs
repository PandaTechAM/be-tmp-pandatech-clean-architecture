using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Delete;

public class DeleteUsersV1Command : ICommand
{
   [PandaPropertyBaseConverter] public List<long> Ids { get; set; } = null!;
}
