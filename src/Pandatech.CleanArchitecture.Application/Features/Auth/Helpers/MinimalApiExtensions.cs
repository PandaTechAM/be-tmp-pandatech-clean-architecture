using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Authenticate;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;

public static class MinimalApiExtensions
{
   public static RouteHandlerBuilder Authorize(this RouteHandlerBuilder builder,
      UserRole minimalUserRole = UserRole.Admin, bool ignoreClientType = false, bool isForcedToChange = false)
   {
      builder.Add(endpointBuilder =>
      {
         var original = endpointBuilder.RequestDelegate;

         endpointBuilder.RequestDelegate = async context =>
         {
            var now = DateTime.UtcNow;
            var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
            var requestContext = context.RequestServices.GetRequiredService<IRequestContext>();
            var sender = context.RequestServices.GetRequiredService<ISender>();
            var requestId = context.TryParseRequestId();
            var languageId = context.TryParseLanguageId();
            var clientType = context.TryParseClientType().ConvertToEnum(!ignoreClientType);
            var accessTokenSignature = context.TryParseAccessTokenSignature(environment);

            var identity = await sender.Send(new AuthenticateV1Query(accessTokenSignature), context.RequestAborted);
            AuthorizationHelper.Authorize(identity, minimalUserRole, isForcedToChange);

            var metaData = new MetaData
            {
               RequestId = requestId, RequestTime = now, LanguageId = languageId, ClientType = clientType
            };

            requestContext.Identity = identity;
            requestContext.MetaData = metaData;
            requestContext.IsAuthenticated = true;

            await original!(context);
            // Post-execution logic
         };
      });

      return builder;
   }
}
