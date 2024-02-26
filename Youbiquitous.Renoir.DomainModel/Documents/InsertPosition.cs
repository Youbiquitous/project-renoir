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
public enum InsertPosition
{
    Bottom = 0,
    Top = 1,
    Specific = 2
}


/// <summary>
/// Dedicated extension methods for the enum
/// </summary>
public static class InsertPositionExtensions
{
    public static bool IsBottom(this InsertPosition position)
    {
        return position == InsertPosition.Bottom;
    }

    public static bool IsTop(this InsertPosition position)
    {
        return position == InsertPosition.Top;
    }

    public static bool IsSpecific(this InsertPosition position)
    {
        return position == InsertPosition.Specific;
    }

    public static string ForDisplay(this InsertPosition position)
    {
        return position switch
        {
            InsertPosition.Bottom => InternalStrings.Text_Position_Bottom,
            InsertPosition.Top => InternalStrings.Text_Position_Top,
            InsertPosition.Specific => InternalStrings.Text_Position_Specific,
            _ => ""
        };
    }
}
