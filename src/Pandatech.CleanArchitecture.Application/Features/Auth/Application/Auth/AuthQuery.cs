using Microsoft.AspNetCore.Http;
using Pandatech.CleanArchitecture.Core.Enums;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Auth;

public record AuthQuery(
   HttpContext HttpContext,
   UserRole MinimalUserRole,
   bool Anonymous,
   bool ForcedToChangePassword,
   bool IgnoreClientType)
   : IQuery;