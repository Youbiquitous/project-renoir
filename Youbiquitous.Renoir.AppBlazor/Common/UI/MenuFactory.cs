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
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Common.UI;

/// <summary>
/// Returns sidebar menus on a per role basis
/// </summary>
public static class MenuFactory
{
    /// <summary>
    /// Returns the default menu
    /// </summary>
    /// <returns></returns>
    public static IList<NavMenuItem> Default(string role = null)
    {
        if (role.IsNullOrWhitespace())
        {
            return new List<NavMenuItem>
            {
                new NavMenuItem(AppMenu.Login, "/login", "fa fa-sign-in") 
            };
        }
            

        var items = new List<NavMenuItem>
        {
            new NavMenuItem(AppMenu.Home, "/", "fa fa-home").Section("APP"),
        };

        if (role == Roles.Contributor)
        {
            items.Add(new NavMenuItem("My days", "/days", "fa fa-calendar"));
        }
        else if (role == Roles.System)
        {
            items.Add(new NavMenuItem("Users", "/users", "fa fa-cloud-download"));
        }

        var group2 = new List<NavMenuItem>
        {
            NavMenuItem.Sep(),
            new NavMenuItem(AppMenu.Welcome, "/welcome", null).Section("PUBLIC LINKS"),
            new(AppMenu.Clicker, "/clicker", "fa fa-plus"),
            new(AppMenu.FetchData, "/fetchdata", "fa fa-cloud-download"),
            new(AppMenu.Additional, "/misc", "fa fa-bomb")
        };
        items.AddRange(group2);

        return items;
    }
}