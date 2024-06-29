using FluentMinimalApiMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pandatech.CleanArchitecture.Api.Helpers;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers.ApiAuth.MinimalApiExtensions;
using Pandatech.CleanArchitecture.Application.Features.UserConfig.CreateOrUpdate;
using Pandatech.CleanArchitecture.Application.Features.UserConfig.Delete;
using Pandatech.CleanArchitecture.Application.Features.UserConfig.Get;
using Pandatech.CleanArchitecture.Core.Enums;
using ResponseCrafter.Extensions;

namespace Pandatech.CleanArchitecture.Api.Endpoints.UserConfig;

public class UserConfigEndpoints : IEndpoint
{
   private const string BaseRoute = "/user";
   private const string TagName = "user-configs";
   private static string RoutePrefix => ApiHelper.GetRoutePrefix(1, BaseRoute);

   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app
         .MapGroup(RoutePrefix)
         .WithTags(TagName)
         .WithGroupName(ApiHelper.GroupNameClean)
         .DisableAntiforgery()
         .WithOpenApi();
      
      groupApp.MapPost("/frontend/configs",
            async ([FromBody] CreateOrUpdateUserConfigCommand request, [FromServices] ISender sender,
               CancellationToken token) =>
            {
               await sender.Send(request, token);
               return TypedResults.Ok();
            })
         .WithSummary("Create or update user frontend configs")
         .Authorize(UserRole.User)
         .ProducesBadRequest();

      groupApp.MapGet("/frontend/configs",
            async ([AsParameters] GetUserConfigsQuery query, [FromServices] ISender sender, CancellationToken token) =>
            {
               var configs = await sender.Send(query, token);
               return TypedResults.Ok(configs);
            })
         .WithSummary("Get user frontend configs")
         .Authorize(UserRole.User)
         .ProducesBadRequest();

      groupApp.MapDelete("/frontend/configs",
            async ([FromBody] DeleteUserConfigsCommand request, [FromServices] ISender sender,
               CancellationToken token) =>
            {
               await sender.Send(request, token);
               return TypedResults.Ok();
            })
         .WithSummary("Delete user frontend configs")
         .Authorize(UserRole.User)
         .ProducesBadRequest();
   }
}
