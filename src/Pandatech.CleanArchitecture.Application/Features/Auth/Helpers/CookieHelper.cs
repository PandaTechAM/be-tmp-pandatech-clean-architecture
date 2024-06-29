﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Auth;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Login;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.RefreshToken;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Extensions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;

public static class CookieHelper
{
   private static void CreateSecureCookies(this List<Cookie> cookies, HttpContext httpContext, string domain,
      IHostEnvironment environment)
   {
      foreach (var cookie in cookies)
      {
         cookie.ExpirationDate ??= DateTime.Now.AddYears(10);

         var cookieOptions = new CookieOptions
         {
            Expires = cookie.ExpirationDate, SameSite = SameSiteMode.None, Secure = true, HttpOnly = true
         };

         if (!environment.IsLocal())
         {
            cookieOptions.Domain = domain;
         }


         if (!environment.IsLocalOrDevelopment())
         {
            cookieOptions.SameSite = SameSiteMode.Strict; //todo check in qa env what should be here
         }


         httpContext.Response.Cookies.Append(cookie.Key, cookie.Value, cookieOptions);
      }
   }

   public static string FormatCookieName(string attributeName, IHostEnvironment environment)
   {
      return !environment.IsProduction()
         ? $"ti_{environment.GetShortEnvironmentName()}_{attributeName}"
         : $"ti_{attributeName}";
   }

   public static void DeleteAllCookies(this HttpContext httpContext, IHostEnvironment environment, string domain)
   {
      foreach (var cookie in httpContext.Request.Cookies)
      {
         if (cookie.Key.Contains(FormatCookieName("device", environment)))
         {
            continue; // Skip deletion for cookies containing the unique device ID
         }


         var cookieOptions = new CookieOptions { Secure = true, HttpOnly = true, SameSite = SameSiteMode.None };

         if (!environment.IsLocal())
         {
            cookieOptions.Domain = domain;
         }

         if (!environment.IsLocalOrDevelopment())
         {
            cookieOptions.SameSite = SameSiteMode.Strict;
         }

         httpContext.Response.Cookies.Delete(cookie.Key, cookieOptions);
      }
   }

   public static void PrepareAndSetCookies(this HttpContext httpContext,
      RefreshTokenCommandResponse mediatorResponse, IHostEnvironment environment,
      string domain)
   {
      var cookies = new IdentityCookies
      {
         AccessTokenSignature = mediatorResponse.AccessTokenSignature,
         RefreshTokenSignature = mediatorResponse.RefreshTokenSignature,
         RefreshTokenExpiresAt = mediatorResponse.RefreshTokenExpiration,
         AccessTokenExpiresAt = mediatorResponse.AccessTokenExpiration
      };
      RefreshIdentityCookies(cookies, httpContext, environment, domain);
   }

   public static void PrepareAndSetCookies(this HttpContext httpContext,
      LoginCommandResponse mediatorResponse, IHostEnvironment environment,
      string domain)
   {
      var cookies = new IdentityCookies
      {
         AccessTokenSignature = mediatorResponse.AccessTokenSignature,
         RefreshTokenSignature = mediatorResponse.RefreshTokenSignature,
         RefreshTokenExpiresAt = mediatorResponse.RefreshTokenExpiration,
         AccessTokenExpiresAt = mediatorResponse.AccessTokenExpiration
      };
      RefreshIdentityCookies(cookies, httpContext, environment, domain);
   }


   private static void RefreshIdentityCookies(IdentityCookies cookies, HttpContext httpContext,
      IHostEnvironment environment, string domain)
   {
      List<Cookie> newCookies =
      [
         new Cookie(FormatCookieName("access_token", environment), cookies.AccessTokenSignature,
            cookies.AccessTokenExpiresAt),
         new Cookie(FormatCookieName("refresh_token", environment), cookies.RefreshTokenSignature,
            cookies.RefreshTokenExpiresAt)
      ];

      newCookies.CreateSecureCookies(httpContext, domain, environment);
   }
}
