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
using Microsoft.AspNetCore.Components.Authorization;

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions;

/// <summary>
/// Helper extension methods for the AuthenticationState class
/// </summary>
public static class StateExtensions
{
    /// <summary>
    /// Role from claims
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static string GetRole(this AuthenticationState state)
    {
        return state?.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
    }

    /// <summary>
    /// Email address from claims
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static string GetEmail(this AuthenticationState state)
    {
        return state?.User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
    }

    /// <summary>
    /// Whether the user is authenticated
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static bool IsAuthenticated(this AuthenticationState state)
    {
        return state.User.Identity?.IsAuthenticated ?? false;
    }
}