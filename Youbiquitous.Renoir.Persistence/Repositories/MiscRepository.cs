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


using Youbiquitous.Renoir.DomainModel.Misc;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for document entities
/// </summary>
public partial class MiscRepository
{
    /// <summary>
    /// Count records in tables
    /// </summary>
    /// <returns></returns>
    public static CountersDescriptor GetCounters()
    {
        using var db = new RenoirDatabase();

        var descriptor = new CountersDescriptor
        {
            TotalNotes = db.ReleaseNotes.Count(),
            TotalRoadmaps = db.Roadmaps.Count(),
            TotalProducts = db.Products.Count(),
            TotalUsers = db.Users.Count()
        };

        return descriptor;
    }
}