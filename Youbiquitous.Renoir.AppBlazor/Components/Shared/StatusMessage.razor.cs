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

namespace Youbiquitous.Renoir.AppBlazor.Components.Shared;

/// <summary>
/// Code-behind class for the StatusMessage component
/// </summary>
public partial class StatusMessage : ComponentBase
{
    public StatusMessage()
    {
        CssClass = "text-danger";
    }

    [Parameter]
    public string Message { get; set; }

    [Parameter]
    public string CssClass { get; set; }
}