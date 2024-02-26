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

namespace Youbiquitous.Renoir.DomainModel.Documents;

/// <summary>
/// Item types for release-note items
/// </summary>
public enum ReleaseNoteItemType
{
    Default = 0,
    Divider = 1
}


/// <summary>
/// Dedicated extension methods for the enum
/// </summary>
public static class ReleaseNoteItemTypeExtensions
{
    public static bool IsDefault(this ReleaseNoteItemType type)
    {
        return type == ReleaseNoteItemType.Default;
    }

    public static bool IsDivider(this ReleaseNoteItemType type)
    {
        return type == ReleaseNoteItemType.Divider;
    }
}
