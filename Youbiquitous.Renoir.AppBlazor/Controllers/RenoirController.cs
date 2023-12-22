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


using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Renoir.Application.Settings;

namespace Youbiquitous.Renoir.AppBlazor.Controllers;

public class RenoirController : Controller
{
    public RenoirController(RenoirSettings settings, IHttpContextAccessor httpAccessor)
    {
        Settings = settings;
        HttpConnection = httpAccessor;
    }

    protected RenoirSettings Settings { get; }
    protected IHttpContextAccessor HttpConnection { get; }
}