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
using Youbiquitous.Renoir.Persistence;

namespace Youbiquitous.Renoir.Application.System;

public partial class SystemService : ApplicationServiceBase
{
    public SystemService(RenoirSettings settings) 
        : base(settings)
    {
    }

    /// <summary>
    /// Ensure all required databases exist, are properly designed and initialized
    /// </summary>
    /// <param name="settings"></param>
    public static void ConfigureDatabases(RenoirSettings settings)
    {
        new RenoirDatabaseInitializer()
            .Initialize(settings.Secrets.RenoirDatabase.Get());
    }
}