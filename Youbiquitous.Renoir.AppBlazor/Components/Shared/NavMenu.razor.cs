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
using Youbiquitous.Renoir.AppBlazor.Common.UI;

namespace Youbiquitous.Renoir.AppBlazor.Components.Shared;

/// <summary>
/// Code-behind class for the navmenu sidebar component
/// </summary>
public class NavMenuComponent : ComponentBase
{
    public NavMenuComponent()
    {
        Items = new List<NavMenuItem>();
    }

    /// <summary>
    /// Header text
    /// </summary>
    [Parameter]
    public string Header { get; set; }

    /// <summary>
    /// List of menu items
    /// </summary>
    [Parameter]
    public IList<NavMenuItem> Items { get; set; }



    /// <summary>
    /// Collapse status
    /// </summary>
    private bool Collapsed = true;

    /// <summary>
    /// CSS class to apply
    /// </summary>
    protected string NavMenuCssClass => Collapsed ? "collapse" : null;


    /// <summary>
    /// Toggles menu collapse status
    /// </summary>
    protected void ToggleNavMenu()
    {
        Collapsed = !Collapsed;
    }

    /// <summary>
    /// Menu initializer method
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
    }
}