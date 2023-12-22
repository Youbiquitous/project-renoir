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
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Youbiquitous.Renoir.AppBlazor.Common.Exceptions;

/// <summary>
/// App-specific class to expose the actual exception occurred
/// </summary>
public class AppErrorBoundary : ErrorBoundary
{
    /// <summary>
    /// Exposes information about the internal unhandled exception
    /// </summary>
    public Exception Current => base.CurrentException;

    protected override Task OnErrorAsync(Exception exception)
    {
        // Log here the way you like

        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (CurrentException is null)
        {
            builder.AddContent(0, ChildContent);
        }
        else if (ErrorContent is not null)
        {
            builder.AddContent(1, ErrorContent(CurrentException));
        }
        else
        {
            // The default error UI doesn't include any content, because:
            // [1] We don't know whether or not you'd be happy to show the stack trace. It depends both on
            //     whether DetailedErrors is enabled and whether you're in production, because even on WebAssembly
            //     you likely don't want to put technical data like that in the UI for end users. A reasonable way
            //     to toggle this is via something like "#if DEBUG" but that can only be done in user code.
            // [2] We can't have any other human-readable content by default, because it would need to be valid
            //     for all languages.
            // Instead, the default project template provides locale-specific default content via CSS. This provides
            // a quick form of customization even without having to subclass this component.
            builder.Clear();
            builder.OpenElement(0, "div");
            builder.AddAttribute(0, "class", "blazor-error-boundary");
            builder.AddContent(0, new MarkupString(CurrentException.Message));
            builder.CloseElement();
        }
    }
}
