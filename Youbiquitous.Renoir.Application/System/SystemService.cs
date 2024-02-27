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
using Youbiquitous.Renoir.DomainModel.Misc;
using Youbiquitous.Renoir.Persistence;
using Youbiquitous.Renoir.Persistence.Repositories;

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

    /// <summary>
    /// Returns counters for the most relevant tables in the system
    /// </summary>
    /// <returns></returns>
    public static CountersDescriptor GetCounters()
    {
        return MiscRepository.GetCounters();
    }
}