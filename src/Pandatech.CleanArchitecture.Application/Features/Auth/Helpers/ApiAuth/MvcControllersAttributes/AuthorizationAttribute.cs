using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Application.Features.Auth.Application.Auth;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Helpers.ApiAuth.MvcControllersAttributes;

public class AuthorizeAttribute(UserRole minimalUserRole) : Attribute, IAsyncAuthorizationFilter
{
   public AuthorizeAttribute() : this(UserRole.Admin)
   {
   }

   public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
   {
      if (IsClassLevelAttributeAndMethodOneExistsToo(context))
      {
         return;
      }

      var anonymous = context.ActionDescriptor.EndpointMetadata.Any(em => em is AnonymousAttribute);
      var ignoreClientType =
         context.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(IgnoreClientTypeAttribute));
      var forcedToChangePwd =
         context.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(ForcedToChangePasswordAttribute));
      var sender = context.HttpContext.RequestServices.GetRequiredService<ISender>();


      await sender.Send(new AuthQuery(context.HttpContext, minimalUserRole, anonymous, forcedToChangePwd,
         ignoreClientType), context.HttpContext.RequestAborted);
   }

   private bool IsClassLevelAttributeAndMethodOneExistsToo(ActionContext context)
   {
      if (context.ActionDescriptor is not ControllerActionDescriptor descriptor)
      {
         return false;
      }

      var classAttributes = descriptor.ControllerTypeInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true)
         .ToList();

      var methodAttributes = descriptor.MethodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();

      if (classAttributes.Count == 0 || methodAttributes.Count == 0)
      {
         return false;
      }

      var isMethodLevel = methodAttributes.Contains(this);

      return !isMethodLevel;
   }
}
