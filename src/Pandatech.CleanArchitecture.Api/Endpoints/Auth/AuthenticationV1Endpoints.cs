using Carter;
using MediatR;
using Pandatech.CleanArchitecture.Api.Helpers;
using Pandatech.CleanArchitecture.Application.Contracts.Auth.IdentityState;
using Pandatech.CleanArchitecture.Application.Contracts.Auth.Login;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.IdentityState;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Login;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RefreshToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeCurrentToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdateOwnPassword;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdatePasswordForced;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Core.Enums;
using ResponseCrafter.Dtos;

namespace Pandatech.CleanArchitecture.Api.Endpoints.Auth;

public class AuthenticationV1Endpoints : ICarterModule
{
   private static string RoutePrefix => ApiHelper.GetRoutePrefix(1, BaseRoute);
   private const string BaseRoute = "/authentication";
   private const string TagName = "authentication";

   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app
         .MapGroup(RoutePrefix)
         .WithTags(TagName)
         .WithGroupName(ApiHelper.GroupNameMain)
         .WithOpenApi();

      groupApp.MapPost("/login",
            async (ISender sender, LoginV1Command command, IHttpContextAccessor httpContextAccessor,
               IHostEnvironment environment, IConfiguration configuration) =>
            {
               var response = await sender.Send(command);
               var clientType = httpContextAccessor.HttpContext!.TryParseClientType().ConvertToEnum();

               if (clientType != ClientType.Browser)
               {
                  return Results.Ok(response);
               }

               var domain = configuration["Security:CookieDomain"]!;
               httpContextAccessor.HttpContext!.PrepareAndSetCookies(response, environment, domain);

               return Results.Ok(response);
            })
         .WithSummary(" \ud83c\udf6a Cookies for the browser and token for the rest of the clients. \ud83c\udf6a")
         .WithDescription(
            "This endpoint is used to authenticate a user. Be aware that the response will be different depending on the client type.")
         .Produces<LoginV1CommandResponse>()
         .Produces<ErrorResponse>(400);


      groupApp.MapPost("/refresh-token",
            async (ISender sender, IHttpContextAccessor httpContextAccessor,
               IHostEnvironment environment, IConfiguration configuration) =>
            {
               var refreshTokenSignature = httpContextAccessor.HttpContext!.TryParseRefreshTokenSignature(environment);
               var response = await sender.Send(new RefreshUserTokenV1Command(refreshTokenSignature));
               var clientType = httpContextAccessor.HttpContext!.TryParseClientType().ConvertToEnum();

               if (clientType != ClientType.Browser)
               {
                  return Results.Ok(response);
               }

               var domain = configuration["Security:CookieDomain"]!;
               httpContextAccessor.HttpContext!.PrepareAndSetCookies(response, environment, domain);

               return Results.Ok(response);
            })
         .WithSummary(" \ud83c\udf6a Cookies for the browser and token for the rest of the clients. \ud83c\udf6a")
         .WithDescription("This endpoint is used to refresh the user token.")
         .Produces<ErrorResponse>(400);


      groupApp.MapGet("/state", async (ISender sender) =>
         {
            var identity = await sender.Send(new GetIdentityStateV1Query());
            return Results.Ok(identity);
         })
         .Authorize(UserRole.User)
         .WithDescription("This endpoint is used to get the current user state.")
         .Produces<IdentityStateV1CommandResponse>();


      groupApp.MapPost("/logout",
            async (ISender sender, IHttpContextAccessor httpContextAccessor, IHostEnvironment environment,
               IConfiguration configuration) =>
            {
               var domain = configuration["Security:CookieDomain"]!;
               await sender.Send(new RevokeCurrentTokenV1Command());
               httpContextAccessor.HttpContext!.DeleteAllCookies(environment, domain);
               return Results.Ok();
            })
         .Authorize(UserRole.User)
         .WithDescription("This endpoint is used to logout the user and delete cookies. \ud83c\udf6a")
         .Produces<ErrorResponse>(404);

      groupApp.MapPatch("/password/force",
            async (ISender sender, UpdatePasswordForcedV1Command command) =>
            {
               await sender.Send(command);
               return Results.Ok();
            })
         .Authorize(UserRole.User, false, true)
         .WithDescription("This endpoint is used to update the user password when it is forced.")
         .Produces<ErrorResponse>(400);

      groupApp.MapPatch("/password/own", async (ISender sender, UpdateOwnPasswordV1Command command) =>
         {
            await sender.Send(command);
            return Results.Ok();
         })
         .Authorize(UserRole.User)
         .WithDescription("This endpoint is used to update the user password from its own profile.")
         .Produces<ErrorResponse>(400);
   }
}
