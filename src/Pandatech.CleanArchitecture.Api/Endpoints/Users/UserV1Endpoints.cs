using BaseConverter.Attributes;
using FluentMinimalApiMapper;
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

public class UserV1Endpoints : IEndpoint
{
   private const string BaseRoute = "/users";
   private const string TagName = "users";
   private static string RoutePrefix => ApiHelper.GetRoutePrefix(1, BaseRoute);

   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app
         .MapGroup(RoutePrefix)
         .WithTags(TagName)
         .WithGroupName(ApiHelper.GroupNameMain)
         .WithOpenApi();

      groupApp.MapPost("", async (ISender sender, [FromBody] CreateUserV1Command command) =>
         {
            await sender.Send(command);
            return Results.Ok();
         })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400);

      groupApp.MapGet("/{id}", async (ISender sender, [PandaParameterBaseConverter] long id) =>
         {
            var user = await sender.Send(new GetUserByIdV1Query(id));
            return Results.Ok(user);
         })
         .Authorize()
         .Produces<GetUserByIdV1QueryResponse>()
         .Produces<ErrorResponse>(404);


      groupApp.MapPut("/{id}",
            async (ISender sender, [PandaParameterBaseConverter] long id, [FromBody] UpdateUserV1Command command) =>
            {
               command.Id = id;
               await sender.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400)
         .Produces<ErrorResponse>(409);


      groupApp.MapPatch("/{id}/password",
            async (ISender sender, [PandaParameterBaseConverter] long id,
               [FromBody] UpdateUserPasswordV1Command command) =>
            {
               command.Id = id;
               await sender.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400)
         .Produces<ErrorResponse>(404);

      groupApp.MapPatch("/{id}/status",
            async (ISender sender, [PandaParameterBaseConverter] long id,
               [FromBody] UpdateUserStatusV1Command command) =>
            {
               command.Id = id;
               await sender.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400)
         .Produces<ErrorResponse>(404);

      groupApp.MapDelete("",
            async (ISender sender, [FromBody] DeleteUsersV1Command command) =>
            {
               await sender.Send(command);
               return Results.Ok();
            })
         .Authorize()
         .Produces(200)
         .Produces<ErrorResponse>(400);
   }
}
