﻿///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 
//

using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.Application.System;
using Youbiquitous.Renoir.DomainModel.Misc;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages;

/// <summary>
/// Code-behind class for the /home view
/// </summary>
public class HomePage : ViewModelBase
{
    /// <summary>
    /// Data holder
    /// </summary>
    public CountersDescriptor Counters { get; set; }

    /// <summary>
    /// Finalize initialization
    /// </summary>
    protected override void OnInitialized()
    {
        Counters = SystemService.GetCounters();
    }
}