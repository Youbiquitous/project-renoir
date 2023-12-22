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
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Shared;

/// <summary>
/// Code-behind class for the ErrorReport component 
/// </summary>
public class ErrorReportComponent : ComponentBase
{
    [Parameter]
    public Exception Exception { get; set; }

    [Parameter]
    public bool ShowInternals { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public string Icon { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string TitleClass { get; set; }

    [Parameter]
    public string TextClass { get; set; }

    /// <summary>
    /// Intelligently determines the text to return for the title of the box
    /// </summary>
    /// <returns></returns>
    public string GetTitle()
    {
        return Exception == null 
            ? Title 
            : ShowInternals ? Exception.Message : (Title ?? AppStrings.Err_Generic);
    }

    /// <summary>
    /// Intelligently determines the text to return for the text of the box
    /// </summary>
    /// <returns></returns>
    public string GetText()
    {
        return Exception == null 
            ? Text
            : ShowInternals ? Exception.StackTrace : (Text ?? Exception.Message);
    }
}