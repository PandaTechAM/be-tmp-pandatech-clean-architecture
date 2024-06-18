using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommand : ICommand
{
   public List<string> Ids { get; set; } = null!;
}
