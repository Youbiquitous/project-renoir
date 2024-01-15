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
using Youbiquitous.Renoir.AppBlazor.Common.Exceptions;
using Youbiquitous.Renoir.Application.Settings;

namespace Youbiquitous.Renoir.AppBlazor.Models;

/// <summary>
/// App-specific base class for modeling custom pages
/// </summary>
public class ViewModelBase : LayoutComponentBase
{
    /// <summary>
    /// Reference to the application settings
    /// </summary>
    [Inject] 
    public RenoirSettings Settings { get; set; }

    /// <summary>
    /// Brings in authentication details (set up in OnInitialized)
    /// </summary>
    [CascadingParameter]
    protected Task<AuthenticationState> AuthState { get; private set; }

    /// <summary>
    /// Error object in case of exceptions
    /// </summary>
    private AppErrorBoundary ErrorBoundary;

    
    /// <summary>
    /// Exposes authentication details
    /// </summary>
    public AuthenticationState Logged { get; private set; }

    /// <summary>
    /// Initialization steps
    /// </summary>
    protected override void OnInitialized()
    {
        Logged = AuthState.Result;
    }

    /// <summary>
    /// Page title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Attempt to recover from exceptions
    /// </summary>
    public void Recover()
    {
        ErrorBoundary?.Recover();
    }

    /// <summary>
    /// Trigger re-rendering (with new data)
    /// </summary>
    protected virtual void Refresh()
    {
        OnInitialized();
        StateHasChanged();
    }
}
