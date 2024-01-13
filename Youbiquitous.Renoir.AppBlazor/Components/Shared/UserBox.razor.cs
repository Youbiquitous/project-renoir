//////////////////////////////////////////////////////////////////////////
//
// Building Enterprise ASP.NET Core 6 Blazor Applications
// Source code
//
// Dino Esposito 
// 2024
// 

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.AppBlazor.Components.Shared;

/// <summary>
/// Code-behind class for the UserBox component
/// </summary>
public class UserBoxComponent : ComponentBase
{
    private AuthenticationState _state;

    public UserBoxComponent()
    {
    }

    [CascadingParameter]
    protected Task<AuthenticationState> AuthState { get; set; }

    protected User CurrentUser { get; set; }

    /// <summary>
    /// Direct access to authentication state
    /// </summary>
    /// <returns></returns>
    public AuthenticationState GetState()
    {
        return _state ??= AuthState.Result;
    }

    protected override void OnInitialized()
    {
        CurrentUser = AccountService.Find(GetState().User.Identity?.Name);
    }
}