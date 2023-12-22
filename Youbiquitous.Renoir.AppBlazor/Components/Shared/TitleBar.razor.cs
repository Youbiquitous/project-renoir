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
/// Code-behind class for the TitleBar component
/// </summary>
public class TitleBarComponent : ComponentBase
{
    [Parameter]
    public string Left { get; set; }

    [Parameter]
    public string LeftClass { get; set; }

    [Parameter]
    public string Right { get; set; }

    [Parameter]
    public string RightClass { get; set; }
}