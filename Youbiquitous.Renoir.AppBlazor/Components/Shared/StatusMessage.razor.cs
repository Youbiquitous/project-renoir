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
        CssClass = "";
        CssClassError = "text-danger";
        CssClassSuccess = "text-success";
        Delay = 0;
        Message = "HAZ";
    }

    /// <summary>
    /// CSS to apply choosing from error/success
    /// </summary>
    private string CssClass { get; set; }

    /// <summary>
    /// Text to display
    /// </summary>
    [Parameter]
    public string Message { get; set; }

    /// <summary>
    /// CSS for a error tone message
    /// </summary>
    [Parameter]
    public string CssClassError { get; set; }

    /// <summary>
    /// CSS for a success tone message
    /// </summary>
    [Parameter]
    public string CssClassSuccess { get; set; }

    /// <summary>
    /// Message timeout
    /// </summary>
    [Parameter]
    public int Delay { get; set; }

    /// <summary>
    /// Present the message (for a given duration)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="success"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public async Task ShowAsync(string message = null, bool success = false, int timeout = 0)
    {
        timeout = timeout <= 0 ? Delay : timeout;
        CssClass = success ? CssClassSuccess : CssClassError;

        Message = message;
        if (timeout <= 0)
            return;
        
        StateHasChanged();
        await Task.Delay(timeout);
        Message = null;
        StateHasChanged();
    }
}