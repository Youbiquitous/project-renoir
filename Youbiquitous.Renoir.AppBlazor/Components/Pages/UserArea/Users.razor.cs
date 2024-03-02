///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.AppBlazor.Models.Input;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.UserArea;

public partial class UsersPage : ViewModelBase
{
    /// <summary>
    /// List of users to render
    /// </summary>
    protected IEnumerable<User> Accounts = new List<User>();

    /// <summary>
    /// Finalize initialization (only first time)
    /// </summary>
    protected override void OnInitialized()
    {
        // Ensure ViewModelBase is initialized
        //base.OnInitialized();
        Accounts = AccountService.Accounts();
    }

    //protected override void OnAfterRender(bool firstRender)
    //{
    //    // Ensure ViewModelBase is initialized
    //    //base.OnInitialized();
    //    if (!firstRender)
    //        Accounts = AccountService.Accounts();
    //}

    /// <summary>
    /// Display the User-Editor modal for a new user
    /// </summary>
    /// <returns></returns>
    protected async Task OpenUserEditor()
    {
        UserEditor.SetUserData(new UserRef());
        UserEditor.SetTitle(AppStrings.Label_TitleNewUser);
        UserEditor.EditMode = false;

        await UserEditor.Window.ShowAsync();
    }

    /// <summary>
    /// Finalize the New-User modal and create a new user
    /// (Controller-level method with direct impact on UI)
    /// </summary>
    protected async Task SaveNewOrExistingUser(UserRef relatedUser)
    {
        // Save user data to the DB
        var author = Logged.GetEmail();
        var response = AccountService.SaveUser(
            relatedUser.UserId,
            relatedUser.DisplayName,
            relatedUser.Email,
            relatedUser.Role,
            relatedUser.Password,
            author);
        if (!response.Success)
        {
            await UserEditor.Statusbar.ShowAsync(response.Message);
            return;
        }

        Refresh();
        await UserEditor.Window.HideAsync();
    }

    /// <summary>
    /// Handler of the UserDeleted event on the table
    /// </summary>
    /// <param name="deleted"></param>
    protected void DeleteExistingUser(User deleted)
    {
        // Remove given user
        var response = AccountService.Delete(deleted.Email);
        if (response.Success)
            Refresh();
    }

    /// <summary>
    /// Handler of the UserEdited event on the table
    /// </summary>
    /// <param name="user"></param>
    protected async Task EditExistingUser(User user)
    {
        var uref = new UserRef().Import(user);
        UserEditor.SetUserData(uref);
        var title = string.Format(AppStrings.Label_TitleEditUser, user.DisplayName);
        UserEditor.SetTitle(title);
        UserEditor.EditMode = true;

        await UserEditor.Window.ShowAsync();
    }
}

