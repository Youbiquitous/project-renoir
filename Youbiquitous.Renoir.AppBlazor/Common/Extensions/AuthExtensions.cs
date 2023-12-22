///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 
//


using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Youbiquitous.Renoir.Application.Auth.Dto;

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions;

public static class AuthExtensions
{
    /// <summary>
    /// Creates the auth cookie for a regular user of the application
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookieInfo"></param>
    /// <returns></returns>
    public static async Task AuthenticateUser(this HttpContext context, AuthenticationResponse cookieInfo)
    {
        try
        {
            // Create the authentication cookie
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, cookieInfo.Email),
                new Claim(ClaimTypes.Role, cookieInfo.Role),
                new Claim(RenoirClaimTypes.DisplayName, cookieInfo.DisplayName),

            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = cookieInfo.StayConnected
                });
        }
        catch (Exception e)
        {
            throw new Exception($"Cookie creation issue: {e.Message}");
        }
    }

    /// <summary>
    /// Read role from claims
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRole(this HttpContext context)
    {
        var role = context.User.FindFirstValue(ClaimTypes.Role);
        return role;
    }
}
