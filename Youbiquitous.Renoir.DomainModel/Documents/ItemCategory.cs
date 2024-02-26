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

namespace Youbiquitous.Renoir.DomainModel.Documents;

/// <summary>
/// Categories for release-note items
/// </summary>
public enum ItemCategory
{
    None = 0,
    Bug = 1,
    Feature = 2,
    Internal = 3
}


/// <summary>
/// Dedicated extension methods for the enum
/// </summary>
public static class ItemCategoryExtensions
{
    public static bool IsBug(this ItemCategory category)
    {
        return category == ItemCategory.Bug;
    }

    public static bool IsFeature(this ItemCategory category)
    {
        return category == ItemCategory.Feature;
    }

    public static bool IsInternal(this ItemCategory category)
    {
        return category == ItemCategory.Internal;
    }

    public static string ForDisplay(this ItemCategory category)
    {
        return category switch
        {
            ItemCategory.None => InternalStrings.Text_Category_None,
            ItemCategory.Internal => InternalStrings.Text_Category_Internal,
            ItemCategory.Bug => InternalStrings.Text_Category_Bug,            
            ItemCategory.Feature => InternalStrings.Text_Category_Feature,
            _ => ""
        };
    }
}
