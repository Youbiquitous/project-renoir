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
using Youbiquitous.Renoir.AppBlazor.Components.Shared;
using Youbiquitous.Renoir.AppBlazor.Models;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.UserArea;

public partial class UsersPage : ViewModelBase
{
    /// <summary>
    /// Reference to the User-Editor modal 
    /// </summary>
    protected UserEditorPopup UserEditor;

    /// <summary>
    /// Reference to the custom component with the table of users
    /// </summary>
    protected UsersTable ListOfUsers;

    /// <summary>
    /// Reference to the custom component with the table of users
    /// </summary>
    protected StatusMessage Status;
}

