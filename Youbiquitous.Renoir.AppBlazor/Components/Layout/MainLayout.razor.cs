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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Youbiquitous.Renoir.AppBlazor.Models;

namespace Youbiquitous.Renoir.AppBlazor.Components.Layout;

/// <summary>
/// Code-behind class for the main layout (includes logged user information)
/// </summary>
public class MainLayoutPage : ViewModelBase
{
    private AuthenticationState _state;

    //[CascadingParameter]
    //protected Task<AuthenticationState> AuthState { get; set; }

    /// <summary>
    /// Direct access to authentication state
    /// </summary>
    /// <returns></returns>
    //public AuthenticationState GetState()
    //{
    //    if (_state == null)
    //        _state = AuthState?.Result;
        
    //    return _state;
    //}
}