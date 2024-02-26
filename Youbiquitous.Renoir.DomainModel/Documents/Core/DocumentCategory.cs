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
/// Categories for documents
/// </summary>
public enum DocumentCategory
{
    TodoList = 0,
    ReleaseNote = 1,
    Roadmap = 2
}


/// <summary>
/// Dedicated extension methods for the enum
/// </summary>
public static class DocumentCategoryExtensions
{
    public static bool IsTodoList(this DocumentCategory category)
    {
        return category == DocumentCategory.TodoList;
    }

    public static bool IsReleaseNote(this DocumentCategory category)
    {
        return category == DocumentCategory.ReleaseNote;
    }

    public static bool IsRoadmap(this DocumentCategory category)
    {
        return category == DocumentCategory.Roadmap;
    }

    public static string ForDisplay(this DocumentCategory category)
    {
        return category switch
        {
            DocumentCategory.TodoList => InternalStrings.Text_Document_TodoList,
            DocumentCategory.ReleaseNote => InternalStrings.Text_Document_ReleaseNote,
            DocumentCategory.Roadmap => InternalStrings.Text_Document_Roadmap,
            _ => ""
        };
    }
}
