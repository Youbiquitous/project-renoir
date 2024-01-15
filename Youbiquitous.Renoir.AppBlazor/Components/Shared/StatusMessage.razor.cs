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
using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.AppBlazor.Components.Shared;

/// <summary>
/// Code-behind class for the StatusMessage component
/// </summary>
public partial class StatusMessage : ComponentBase
{
    public StatusMessage()
    {
        CssClass = "text-danger";
        Delay = 0;
    }

    [Parameter]
    public string Message { get; set; }

    [Parameter]
    public string CssClass { get; set; }

    [Parameter]
    public int Delay { get; set; }

    /// <summary>
    /// Present the message (for a given duration)
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task ShowAsync(string message = null)
    {
        Message = message;
        if (Delay <= 0)
            return;
        
        StateHasChanged();
        await Task.Delay(Delay);
        Message = null;
        StateHasChanged();
    }
}