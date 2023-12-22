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
    /// Error object in case of exceptions
    /// </summary>
    private AppErrorBoundary ErrorBoundary;

    /// <summary>
    /// Page title
    /// </summary>
    public string Title { get; set; }

    public void Recover()
    {
        ErrorBoundary?.Recover();
    }
}
