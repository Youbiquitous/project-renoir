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


using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.AppBlazor.Common.UI;

/// <summary>
/// Describes a menu item to show in the app sidebar
/// </summary>
public class NavMenuItem
{
    public static NavMenuItem Sep()
    {
        return new NavMenuItem { Divider = true };
    }

    public NavMenuItem()
    {
    }

    public NavMenuItem(string l, string u, string i)
    {
        Label = l;
        Url = u;
        Icon = i;
    }

    /// <summary>
    /// Add a section name to the menu item
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public NavMenuItem Section(string name)
    {
        SectionName = name;
        return this;
    }

    /// <summary>
    /// Menu item label
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Menu item URL
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Icon class
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Maintains divider status
    /// </summary>
    public bool Divider { get; set; }

    /// <summary>
    /// Whether the menu item is a divider
    /// </summary>
    /// <returns></returns>
    public bool IsDivider()
    {
        return Divider;
    }

    /// <summary>
    /// Title of a section
    /// </summary>
    public string SectionName { get; set; }

    /// <summary>
    /// Whether the menu item is a divider
    /// </summary>
    /// <returns></returns>
    public bool HasSectionName()
    {
        return !SectionName.IsNullOrWhitespace();
    }
}