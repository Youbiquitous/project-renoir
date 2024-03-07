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

using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.DomainModel.WorkInProgress;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for WorkItem entity
/// </summary>
public partial class WorkItemRepository
{
    /// <summary>
    /// Edit/add a new work item
    /// </summary>
    /// <param name="workItem"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse Upsert(WorkItem workItem, string author)
    {
        using var db = new RenoirDatabase();
        var found = db.WorkItems.FirstOrDefault(w => w.Id == workItem.Id);

        return CommandResponse.Ok(); 
    }


    /// <summary>
    /// Delete (logically) a work item 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static CommandResponse Delete(long id)
    {
        using var db = new RenoirDatabase();

        return CommandResponse.Ok(); 
    }
}