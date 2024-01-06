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
using Microsoft.JSInterop;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.Application.Auth.Dto;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages;

/// <summary>
/// Code-behind class for the /home view
/// </summary>
public class HomePage : ViewModelBase
{
    public HomePage()
    {
    }

    /// <summary>
    /// Finalize initialization
    /// </summary>
    protected override void OnInitialized()
    {
        // Ideally clean up the side menu

    }
}