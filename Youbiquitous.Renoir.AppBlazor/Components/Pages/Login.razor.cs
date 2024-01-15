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
using Microsoft.JSInterop;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.Application.Auth.Dto;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages;

/// <summary>
/// Code-behind class for the /login view
/// </summary>
public class LoginPage : ViewModelBase
{
    public LoginPage()
    {
        LoginRequest = new AuthenticationRequest();
    }
    
    /// <summary>
    /// API to navigate to pages
    /// </summary>
    [Inject]
    protected NavigationManager GotoManager { get; set; }

    [Inject]
    protected IJSRuntime JS { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Whether authentication is ongoing
    /// </summary>
    public bool IsProcessing { get; private set; }

    /// <summary>
    /// Any error message to show in the page
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Container for login input data
    /// </summary>
    public AuthenticationRequest LoginRequest { get; set; }

    /// <summary>
    /// Orchestrates the process of verifying credentials
    /// </summary>
    /// <returns></returns>
    protected async Task Submit()
    {
        // Invalid form status 
        if (!LoginRequest.IsValid())
        {
            ErrorMessage = AppMessages.Err_IncompleteCredentials;
            return;
        }

        // Form submission
        try
        {
            IsProcessing = true;
            var raw = await JS.InvokeAsync<string>("postForm", "#login-form");
            if (raw.IsNullOrWhitespace())
            {
                ErrorMessage = "Method call failed";
                return;
            }
            var response = raw.ToJsonObject<AuthenticationResponse>();
            if (response.Success)
            {
                GotoManager.NavigateTo(ReturnUrl, true);
                return;
            }

            ErrorMessage = response.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsProcessing = false;
        }
    }

    /// <summary>
    /// Hide error messages within the login form
    /// </summary>
    protected void HideErrorMessage()
    {
        ErrorMessage = "";
    }

    /// <summary>
    /// Finalize initialization
    /// </summary>
    protected override void OnInitialized()
    {
        ReturnUrl ??= "/";
    }
}