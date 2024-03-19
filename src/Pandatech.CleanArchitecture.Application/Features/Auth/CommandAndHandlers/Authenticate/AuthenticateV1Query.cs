using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Authenticate;

public record AuthenticateV1Query(string AccessTokenSignature) : IQuery<Identity>;

