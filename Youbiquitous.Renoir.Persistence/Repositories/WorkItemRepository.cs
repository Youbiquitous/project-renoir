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

using Youbiquitous.Renoir.DomainModel.WorkInProgress;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for WorkItem entity
/// </summary>
public partial class WorkItemRepository
{
    /// <summary>
    /// Retrieves the list of all valid work items
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<WorkItem> All()
    {
        using var db = new RenoirDatabase();
        return db.WorkItems.Where(w => !w.Deleted).ToList(); 
    }
}