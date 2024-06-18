using BaseConverter.Attributes;
using FluentMinimalApiMapper;
using GridifyExtensions.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pandatech.CleanArchitecture.Api.Helpers;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers.ApiAuth.MinimalApiExtensions;
using Pandatech.CleanArchitecture.Application.Features.User.Application.Create;
using Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;
using Pandatech.CleanArchitecture.Application.Features.User.Application.GetColumnDistinctValues;
using Pandatech.CleanArchitecture.Application.Features.User.Application.GetUser;
using Pandatech.CleanArchitecture.Application.Features.User.Application.GetUsers;
using Pandatech.CleanArchitecture.Application.Features.User.Application.Update;
using Pandatech.CleanArchitecture.Application.Features.User.Application.UpdatePassword;
using Pandatech.CleanArchitecture.Application.Features.User.Application.UpdateStatus;
using Pandatech.CleanArchitecture.Core.Entities;
using ResponseCrafter.Extensions;

namespace Pandatech.CleanArchitecture.Api.Endpoints.Users;

public class UserEndpoints : IEndpoint
{
   private const string BaseRoute = "/users";
   private const string TagName = "users";
   private static string RoutePrefix => ApiHelper.GetRoutePrefix(1, BaseRoute);

   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app
         .MapGroup(RoutePrefix)
         .WithTags(TagName)
         .WithGroupName(ApiHelper.GroupNameClean)
         .WithOpenApi();

      groupApp.MapPost("", async (ISender sender, [FromBody] CreateUserCommand command, CancellationToken token) =>
         {
            await sender.Send(command, token);
            return TypedResults.Ok();
         })
         .Authorize()
         .ProducesErrorResponse(400);

      groupApp.MapGet("/{id}", async (ISender sender, [PandaParameterBaseConverter] long id, CancellationToken token) =>
         {
            var user = await sender.Send(new GetUserQuery(id), token);
            return TypedResults.Ok(user);
         })
         .Authorize()
         .ProducesErrorResponse(404);


      groupApp.MapPut("/{id}",
            async (ISender sender, [PandaParameterBaseConverter] long id, [FromBody] UpdateUserCommand command,
               CancellationToken token) =>
            {
               command.Id = id;
               await sender.Send(command, token);
               return TypedResults.Ok();
            })
         .Authorize()
         .ProducesErrorResponse(400)
         .ProducesErrorResponse(409);


      groupApp.MapPatch("/{id}/password",
            async (ISender sender, [PandaParameterBaseConverter] long id,
               [FromBody] UpdateUserPasswordCommand command, CancellationToken token) =>
            {
               command.Id = id;
               await sender.Send(command, token);
               return TypedResults.Ok();
            })
         .Authorize()
         .ProducesErrorResponse(400)
         .ProducesErrorResponse(404);

      groupApp.MapPatch("/{id}/status",
            async (ISender sender, [PandaParameterBaseConverter] long id,
               [FromBody] UpdateUserStatusCommand command, CancellationToken token) =>
            {
               command.Id = id;
               await sender.Send(command, token);
               return TypedResults.Ok();
            })
         .Authorize()
         .ProducesErrorResponse(400)
         .ProducesErrorResponse(404);

      groupApp.MapDelete("",
            async (ISender sender, [FromBody] DeleteUsersCommand command, CancellationToken token) =>
            {
               await sender.Send(command, token);
               return TypedResults.Ok();
            })
         .Authorize()
         .ProducesErrorResponse(400);

      groupApp.MapGet("", async ([AsParameters] GetUsersQuery request, ISender sender, CancellationToken token) =>
         {
            var users = await sender.Send(request, token);
            return TypedResults.Ok(users);
         })
         .Authorize()
         .ProducesErrorResponse(400);

      groupApp.MapGet("/column/distinct",
            async ([AsParameters] GetUserColumnDistinctValuesQuery query, ISender sender, CancellationToken token) =>
            {
               var distinctValues = await sender.Send(query, token);
               return TypedResults.Ok(distinctValues);
            })
         .Authorize()
         .ProducesErrorResponse(400);

      groupApp.MapGet("/filters", () => TypedResults.Ok(QueryableExtensions.GetMappings<User>()))
         .Authorize()
         .WithSummary("Get filter technical information")
         .ProducesErrorResponse(400);
   }
}
