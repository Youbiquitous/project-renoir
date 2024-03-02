///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 


namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.Documents.ReleaseNotes;

public partial class ReleaseNotesPage 
{
    /// <summary>
    /// Reference to the Document-Editor modal 
    /// </summary>
    protected ReleaseNoteEditorPopup DocumentEditor;

    /// <summary>
    /// Reference to the custom component with the table of release note docs
    /// </summary>
    protected RnList ListOfDocuments;
}

