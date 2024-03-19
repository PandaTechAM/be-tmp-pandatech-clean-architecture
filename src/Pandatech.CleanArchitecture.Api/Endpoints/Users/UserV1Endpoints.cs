using BaseConverter.Attributes;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pandatech.CleanArchitecture.Api.Helpers;
using Pandatech.CleanArchitecture.Application.Contracts.User.GetById;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Application.Features.User.Create;
using Pandatech.CleanArchitecture.Application.Features.User.Delete;
using Pandatech.CleanArchitecture.Application.Features.User.GetById;
using Pandatech.CleanArchitecture.Application.Features.User.Update;
using Pandatech.CleanArchitecture.Application.Features.User.UpdatePassword;
using Pandatech.CleanArchitecture.Application.Features.User.UpdateStatus;
using ResponseCrafter.Dtos;

namespace Pandatech.CleanArchitecture.Api.Endpoints.Users;

public class UserV1Endpoints : ICarterModule
{
   private static string RoutePrefix => ApiHelper.GetRoutePrefix(1, BaseRoute);
   private const string BaseRoute = "/users";
   private const string TagName = "users";

   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app
         .MapGroup(RoutePrefix)
         .WithTags(TagName)
         .WithGroupName(ApiHelper.GroupNameMain)
         .WithOpenApi();

      groupApp.MapPost("", async (ISender mediator, [FromBody] CreateUserV1Command command) =>
         {
            await mediator.Send(command);
            return Results.Ok();
         })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400);

      groupApp.MapGet("/{id}", async (ISender mediator, [PandaParameterBaseConverter] long id) =>
         {
            var user = await mediator.Send(new GetUserByIdV1Query(id));
            return Results.Ok(user);
         })
         .Authorize()
         .Produces<GetUserByIdV1QueryResponse>()
         .Produces<ErrorResponse>(404);


      groupApp.MapPut("/{id}",
            async (ISender mediator, [PandaParameterBaseConverter] long id, [FromBody] UpdateUserV1Command command) =>
            {
               command.Id = id;
               await mediator.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400)
         .Produces<ErrorResponse>(409);


      groupApp.MapPatch("/{id}/password",
            async (ISender mediator, [PandaParameterBaseConverter] long id,
               [FromBody] UpdateUserPasswordV1Command command) =>
            {
               command.Id = id;
               await mediator.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400)
         .Produces<ErrorResponse>(404);

      groupApp.MapPatch("/{id}/status",
            async (ISender mediator, [PandaParameterBaseConverter] long id,
               [FromBody] UpdateUserStatusV1Command command) =>
            {
               command.Id = id;
               await mediator.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400)
         .Produces<ErrorResponse>(404);

      groupApp.MapDelete("",
            async (ISender mediator, [FromBody] DeleteUsersV1Command command) =>
            {
               await mediator.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400);
   }
}
