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

using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence.Repositories;

public partial class RoadmapRepository
{
    /// <summary>
    /// Remove a release note and all of its items
    /// </summary>
    /// <param name="docId"></param>
    /// <returns></returns>
    public static CommandResponse Delete(long docId)
    {
        using var db = new RenoirDatabase();
        db.Roadmaps
            .Where(rn => rn.RefId == docId)
            .ExecuteDelete();
        return CommandResponse.Ok();
    }

    /// <summary>
    /// Create a new roadmap container 
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static CommandResponse Create(Roadmap doc)
    {
        using var db = new RenoirDatabase();
        var found = db.Roadmaps.FirstOrDefault(rn => rn.Version == doc.Version);
        if (found is not null)
            return CommandResponse.Fail().AddMessage(AppMessages.Err_VersionAlreadyFound);

        db.Roadmaps.Add(doc);
        return db.TrySaveChanges();
    }

    /// <summary>
    /// Edit a release note adding/editing/removing items
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static CommandResponse Update(Roadmap doc)
    {
        using var db = new RenoirDatabase();
        var found = db.Roadmaps
            .Include(rn => rn.Items)
            .FirstOrDefault(rn => rn.Version == doc.Version);
        if (found is null)
            return CommandResponse.Fail()
                .AddMessage(AppMessages.Err_VersionNotFound);

        found.Import(doc);

        return db.TrySaveChanges();
    }
}