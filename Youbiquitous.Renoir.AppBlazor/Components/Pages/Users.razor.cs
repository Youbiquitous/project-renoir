///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages;

public class UsersPage : ViewModelBase
{
    /// <summary>
    /// Reference to the New-User modal 
    /// </summary>
    protected Modal NewUserModal = default!;

    /// <summary>
    /// List of users to render
    /// </summary>
    protected IEnumerable<User> Accounts = default!;

    /// <summary>
    /// Finalize initialization
    /// </summary>
    protected override void OnInitialized()
    {
        Accounts = AccountService.Accounts();
        NewUser = new NewUser();
    }

    /// <summary>
    /// Display thew New-User modal
    /// </summary>
    /// <returns></returns>
    protected async Task ShowNewUserModal()
    {
        await NewUserModal.ShowAsync();
    }

    /// <summary>
    /// Container to move data to/from the modal
    /// </summary>
    [SupplyParameterFromForm]
    protected NewUser NewUser { get; set; }

    /// <summary>
    /// Message for the user
    /// </summary>
    protected string NewUserStatusMessage { get; set; }

    /// <summary>
    /// Finalize the New-User modal and create a new user
    /// </summary>
    protected async Task CreateNewUser()
    {
        if (!NewUser.IsValid())
        {
            NewUserStatusMessage = "Invalid content";
            await Task.Delay(2500);
            NewUserStatusMessage = "";
            return;
        }

        // Add new user to the DB
        var response = AccountService.CreateNew(NewUser.Email, NewUser.Role, NewUser.DisplayName);
        await NewUserModal.HideAsync();
    }
}



/// <summary>
/// DTO to bring data to and from the New-User form
/// </summary>
public class NewUser
{
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public bool IsValid()
    {
        return !DisplayName.IsNullOrWhitespace() &&
               Email.IsValidEmail();
    }
}