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

namespace Youbiquitous.Renoir.DomainModel.Documents.Core;

/// <summary>
/// Categories for release-note items
/// </summary>
public enum DocumentItemCategory
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
    public static bool IsBug(this DocumentItemCategory category)
    {
        return category == DocumentItemCategory.Bug;
    }

    public static bool IsFeature(this DocumentItemCategory category)
    {
        return category == DocumentItemCategory.Feature;
    }

    public static bool IsInternal(this DocumentItemCategory category)
    {
        return category == DocumentItemCategory.Internal;
    }

    public static string ForDisplay(this DocumentItemCategory category)
    {
        return category switch
        {
            DocumentItemCategory.None => InternalStrings.Text_Category_None,
            DocumentItemCategory.Internal => InternalStrings.Text_Category_Internal,
            DocumentItemCategory.Bug => InternalStrings.Text_Category_Bug,
            DocumentItemCategory.Feature => InternalStrings.Text_Category_Feature,
            _ => ""
        };
    }
}
