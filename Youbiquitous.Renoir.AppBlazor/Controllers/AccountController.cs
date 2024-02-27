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


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.Application.Auth;
using Youbiquitous.Renoir.Application.Auth.Dto;
using Youbiquitous.Renoir.Application.Settings;

namespace Youbiquitous.Renoir.AppBlazor.Controllers;

public class AccountController : RenoirController
{
    private readonly AuthService _auth;
    public AccountController(RenoirSettings settings, IHttpContextAccessor httpAccessor) 
        : base(settings, httpAccessor)
    {
        _auth = new AuthService(settings);
    }

    /// <summary>
    /// Orchestrates the sign-in process for a user that submits credentials
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("signin")]
    public async Task<IActionResult> TrySignIn(AuthenticationRequest input)
    {
        if (input is null || !input.IsValid())
        {
            var fail = AuthenticationResponse.Fail().AddMessage("Invalid input");
            return Json(fail);
        }

        // Validate login data
        var response = _auth.TrySignIn(input);
        if (!response.Success)
            return Json(response);

        // Create the authentication cookie and redirect to destination
        await HttpContext.AuthenticateUser(response);
        var destination = input.ReturnUrl ?? GetHomeUrl(response);
        response.Redirect(destination);
        return Json(response);
    }

    /// <summary>
    /// Logs out the connected user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/");
    }

    /// <summary>
    /// Test exceptions
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [Route("/throw")]
    public IActionResult Throw()
    {
        throw new Exception("Exception thrown for testing purposes");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static string GetHomeUrl(AuthenticationResponse response)
    {
        return response.Role.EqualsAny("system") 
            ? "/home" 
            : "/myproducts";
    }
    public static string GetHomeUrl(AuthenticationState state)
    {
        var role = state.GetRole();
        return role.EqualsAny("system") 
            ? "/home" 
            : "/myproducts";
    }
}