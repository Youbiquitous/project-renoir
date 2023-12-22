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

using Youbiquitous.Renoir.Application.Settings;

namespace Youbiquitous.Renoir.Application;

public class ApplicationServiceBase
{
    protected ApplicationServiceBase(RenoirSettings settings) 
    {
        Settings = settings;
    }


    /// <summary>
    /// Reference to application settings
    /// </summary>
    public RenoirSettings Settings { get; }
}