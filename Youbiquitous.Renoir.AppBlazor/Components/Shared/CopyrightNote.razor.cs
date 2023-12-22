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
/// Code-behind class for the CopyrightNote component
/// </summary>
public class CopyrightNoteComponent : ComponentBase
{
    [Parameter]
    public string FirstLine { get; set; }

    [Parameter]
    public string SecondLine { get; set; }
}