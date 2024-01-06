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


using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Common.UI;

/// <summary>
/// Returns sidebar menus on a per role basis
/// </summary>
public static class MenuFactory
{
    /// <summary>
    /// Returns the menu for a given role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static IList<NavMenuItem> ForRole(string role = null)
    {
        return role switch
        {
            Roles.Contributor => ContributorMenu(),
            Roles.Owner => OwnerMenu(),
            Roles.System => SystemMenu(),
            _ => AnonymousMenu()
        };
    }

    /// <summary>
    /// Reserved to anonymous users
    /// </summary>
    /// <returns></returns>
    private static IList<NavMenuItem> AnonymousMenu()
    {
        return new List<NavMenuItem>
        {
            new(AppMenu.Login, "/login", "fa fa-sign-in")
        };
    }

    /// <summary>
    /// Reserved to users with the role of Contributor 
    /// </summary>
    /// <returns></returns>
    private static IList<NavMenuItem> ContributorMenu()
    {
        return new List<NavMenuItem>
        {
            new(AppMenu.MyWork, "/mywork", "fa fa-sign-in")
        };
    }

    /// <summary>
    /// Reserved to users with the role of Owner 
    /// </summary>
    /// <returns></returns>
    private static IList<NavMenuItem> OwnerMenu()
    {
        return new List<NavMenuItem>
        {
            new(AppMenu.MyProducts, "/myproducts", "fa fa-sign-in")
        };
    }

    
    /// <summary>
    /// Reserved to users with the role of System 
    /// </summary>
    /// <returns></returns>
    private static IList<NavMenuItem> SystemMenu()
    {
        return new List<NavMenuItem>
        {
            new(AppMenu.Products, "/products", "fa fa-sign-in"),
            new(AppMenu.Users, "/users", "fa fa-user")
        };
    }
}
