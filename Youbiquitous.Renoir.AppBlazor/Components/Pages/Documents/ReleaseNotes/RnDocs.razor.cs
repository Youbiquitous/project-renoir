///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using Microsoft.AspNetCore.Components;
using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.AppBlazor.Models.Input;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.Documents.ReleaseNotes;

public partial class ReleaseNotesPage : ViewModelBase
{
    /// <summary>
    /// Selected user and related products
    /// </summary>
    public User Current { get; set; }

    /// <summary>
    /// List of documents to present
    /// </summary>
    protected IEnumerable<ReleaseNote> Documents;
    
    /// <summary>
    /// Product ID from query string
    /// </summary>
    [Parameter]
    [SupplyParameterFromQuery(Name = "rn")]
    public long SelectedProductId { get; set; }

    /// <summary>
    /// Finalize initialization (only first time)
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Current = AccountService.Find(Logged.GetEmail());
        Documents = DocumentService.ReleaseNotesFor(Current.UserId, SelectedProductId)?.Documents ?? new List<ReleaseNote>();
    }

    /// <summary>
    /// Catch error conversion in the assignment of query string parameters
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        try
        {
            return base.SetParametersAsync(parameters);
        }
        catch  
        {
            SelectedProductId = 0;
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Handler of the DocumentDeleted event on the table
    /// </summary>
    /// <param name="document"></param>
    protected void DeleteExistingDocument(ReleaseNote document)
    {
        // Remove given document
        var response = DocumentService.DeleteReleaseNote(document.RefId);
        if (response.Success)
            Refresh();
    }

    /// <summary>
    /// Display the Document-Editor modal for a new release note document
    /// </summary>
    /// <returns></returns>
    protected async Task NewDocumentEditor()
    {
        DocumentEditor.SetTitle(AppStrings.Label_NewReleaseNote);
        DocumentEditor.SetUserData(new DocRef());
        await DocumentEditor.Window.ShowAsync();
    }

    /// <summary>
    /// Reopen the document modal for editing version and comment 
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    protected async Task EditDocumentEditor(ReleaseNote document)
    {
        DocumentEditor.SetTitle(AppStrings.Label_EditReleaseNote);
        DocumentEditor.SetUserData(new DocRef(document.RefId, document.Version, document.ReleaseDate, document.Notes));
        await DocumentEditor.Window.ShowAsync();
    }

    /// <summary>
    /// Finalize the New-Document modal and create a new release-note (or edit)
    /// </summary>
    /// <param name="rn"></param>
    /// <returns></returns>
    protected async Task SaveDocument(DocRef rn)
    {
        // Save user data to the DB
        var author = Logged.GetEmail();
        var response = rn.RefId == 0
            ? DocumentService.NewReleaseNote(SelectedProductId, rn.Version, rn.ReleaseDate, rn.Notes, author)
            : DocumentService.SaveReleaseNote(rn.RefId, SelectedProductId, rn.Version, rn.ReleaseDate, rn.Notes, author);
        if (!response.Success)
        {
            await DocumentEditor.Statusbar.ShowAsync(response.Message);
            return;
        }

        Refresh();
        await DocumentEditor.Window.HideAsync();
    }
}