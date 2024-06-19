using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommand : ICommand
{
   [PropertyBaseConverter] public List<long> Ids { get; set; } = null!;
}
