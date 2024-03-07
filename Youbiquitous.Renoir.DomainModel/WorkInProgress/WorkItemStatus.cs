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

using Youbiquitous.Renoir.DomainModel.Utils;

namespace Youbiquitous.Renoir.DomainModel.WorkInProgress;

/// <summary>
/// Status of a work item  
/// </summary>
public enum WorkItemStatus
{
    Finished = 1,
    Scheduled = 2,
    Pending = 3,
    Fixed = 4
}

public static class WorkItemStatusExtensions
{
    public static bool IsFinished(this WorkItemStatus wis)
    {
        return wis == WorkItemStatus.Finished;
    }
    public static bool IsScheduled(this WorkItemStatus wis)
    {
        return wis == WorkItemStatus.Scheduled;
    }
    public static bool IsPending(this WorkItemStatus wis)
    {
        return wis == WorkItemStatus.Pending;
    }
    public static bool IsFixed(this WorkItemStatus wis)
    {
        return wis == WorkItemStatus.Fixed;
    }

    public static string ForDisplay(this WorkItemStatus wis)
    {
        return wis switch
        {
            WorkItemStatus.Finished => InternalStrings.Text_Item_Finished,
            WorkItemStatus.Scheduled => InternalStrings.Text_Item_Scheduled,
            WorkItemStatus.Fixed => InternalStrings.Text_Item_Fixed,
            WorkItemStatus.Pending => InternalStrings.Text_Item_Pending,
            _ => string.Empty,
        };
    }
}