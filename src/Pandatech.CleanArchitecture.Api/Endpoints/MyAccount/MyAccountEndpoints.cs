﻿using FluentMinimalApiMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pandatech.CleanArchitecture.Api.Helpers;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers.ApiAuth.MinimalApiExtensions;
using Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.PersonalInformation;
using Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.RevokeCurrentToken;
using Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.UpdateOwnPassword;
using Pandatech.CleanArchitecture.Core.Enums;
using ResponseCrafter.Extensions;

namespace Pandatech.CleanArchitecture.Api.Endpoints.MyAccount;

public class MyAccountEndpoints : IEndpoint
{
   private const string BaseRoute = "/my-account";
   private const string TagName = "my-account";
   private static string RoutePrefix => ApiHelper.GetRoutePrefix(1, BaseRoute);

   public void AddRoutes(IEndpointRouteBuilder app)
   {
      var groupApp = app
         .MapGroup(RoutePrefix)
         .WithTags(TagName)
         .WithGroupName(ApiHelper.GroupNameClean)
         .WithOpenApi();

      groupApp.MapGet("/personal-information", async (ISender sender, CancellationToken token) =>
         {
            var personalInformation = await sender.Send(new GetPersonalInformationQuery(), token);
            return TypedResults.Ok(personalInformation);
         })
         .WithSummary("Get personal information")
         .Authorize(UserRole.User);

      groupApp.MapPatch("/password",
            async (ISender sender, [FromBody] UpdateOwnPasswordCommand command, CancellationToken token) =>
            {
               await sender.Send(command, token);
               return TypedResults.Ok();
            })
         .Authorize(UserRole.User)
         .WithDescription("This endpoint is used to update the user password from its own profile.")
         .ProducesErrorResponse(400);


      groupApp.MapPost("/logout",
            async (ISender sender, IHttpContextAccessor httpContextAccessor, IHostEnvironment environment,
               IConfiguration configuration, CancellationToken token) =>
            {
               var domain = configuration["Security:CookieDomain"]!;
               await sender.Send(new LogoutCommand(), token);
               httpContextAccessor.HttpContext!.DeleteAllCookies(environment, domain);
               return TypedResults.Ok();
            })
         .Authorize(UserRole.User)
         .WithDescription("This endpoint is used to logout the user and delete cookies. \ud83c\udf6a")
         .ProducesErrorResponse(404);
   }
}
