///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using Microsoft.AspNetCore.Components;
using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.Personal;

public partial class MyProductsPage : ViewModelBase
{
    /// <summary>
    /// Selected user and related products
    /// </summary>
    public User Current { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [Parameter]
    public long SelectedProductId { get; set; }


    /// <summary>
    /// Finalize initialization (only first time)
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Current = AccountService.Find(Logged.GetEmail(), includeProducts: true);
    }
}

