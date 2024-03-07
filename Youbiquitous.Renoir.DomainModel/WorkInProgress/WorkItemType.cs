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
/// Tracked work item (things done, to-do, bugs)
/// </summary>
public enum WorkItemType
{
    Feature = 1,
    Todo = 2,
    Bug = 3
}

public static class WorkItemTypeExtensions
{
    public static bool IsFeature(this WorkItemType wit)
    {
        return wit == WorkItemType.Feature;
    }
    public static bool IsTodo(this WorkItemType wit)
    {
        return wit == WorkItemType.Todo;
    }
    public static bool IsBug(this WorkItemType wit)
    {
        return wit == WorkItemType.Bug;
    }

    public static string ForDisplay(this WorkItemType wis)
    {
        return wis switch
        {
            WorkItemType.Feature => InternalStrings.Text_Item_Feature,
            WorkItemType.Todo => InternalStrings.Text_Item_Todo,
            WorkItemType.Bug => InternalStrings.Text_Item_Bug,
            _ => string.Empty,
        };
    }
}