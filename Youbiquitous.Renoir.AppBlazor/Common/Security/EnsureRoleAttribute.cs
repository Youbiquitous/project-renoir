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


using Microsoft.AspNetCore.Mvc.Filters;

namespace Youbiquitous.Renoir.AppBlazor.Common.Security;

/// <summary>
/// If set in two places (class/methods), it seems the first wins
/// </summary>
public class EnsureRoleAttribute : ActionFilterAttribute
{
    public EnsureRoleAttribute(params string[] roles)
    {
        Roles = roles;
    }

    private string[] Roles { get; }

    /// <summary>
    /// Control the start of the action
    /// </summary>
    /// <param name="filterContext"></param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);

        //var loggedUser = filterContext.HttpContext.User.Logged();


        // If no roles specified, then it means "all roles enabled"
        if (Roles.Length == 0)
            return;

        var shouldThrow = true;
        foreach (var expectedRole in Roles)
        {
            var hasMatchingRole = filterContext.HttpContext.User.IsInRole(expectedRole);
            if (!hasMatchingRole) 
                continue;

            shouldThrow = false;
            break;
        }

        if (shouldThrow)
        {
            throw new UnauthorizedAccessException(); 
        }
    }
}