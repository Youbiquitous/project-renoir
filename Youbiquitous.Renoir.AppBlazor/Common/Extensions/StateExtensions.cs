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
    /// Full user object currently logged (if any)
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static string GetRole(this AuthenticationState state)
    {
        return state?.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
    }
}