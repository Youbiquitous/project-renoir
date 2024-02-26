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

namespace Youbiquitous.Renoir.DomainModel.Documents.Core;

/// <summary>
/// Item types for document items
/// </summary>
public enum DocumentItemType
{
    Default = 0,
    Divider = 1
}


/// <summary>
/// Dedicated extension methods for the enum
/// </summary>
public static class DocumentItemTypeExtensions
{
    public static bool IsDefault(this DocumentItemType type)
    {
        return type == DocumentItemType.Default;
    }

    public static bool IsDivider(this DocumentItemType type)
    {
        return type == DocumentItemType.Divider;
    }
}
