using System.ComponentModel;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Login;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Login;

public class LoginCommand : ICommand<LoginCommandResponse>
{
   [DefaultValue("admin@admin.com")]
   public required string Username { get; set; }

   [DefaultValue("Qwertyui123@")]
   public required string Password { get; set; }
}