///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Components.Layout;
using Youbiquitous.Renoir.AppBlazor.Components.Shared;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.Application.Renderers;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Documents.Core;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.Documents.ReleaseNotes;

public partial class ReleaseNoteEditorPage : MainLayoutPage
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    /// <summary>
    /// Selected user and related products
    /// </summary>
    public User Current { get; set; }

    [Parameter]
    public string StatusMessage { get; set; }
    public StatusMessage Statusbar { get; set; }


    /// <summary>
    /// List of documents to present
    /// </summary>
    protected ReleaseNote RelatedDocument;

    /// <summary>
    /// Number of items read from storage (initially)
    /// </summary>
    protected int InitialNumberOfItems { get; set; }
    
    /// <summary>
    /// Parent document ID from query string
    /// </summary>
    [Parameter]
    [SupplyParameterFromQuery(Name = "rnid")]
    public long ParentDocumentId { get; set; }

    /// <summary>
    /// Finalize initialization (only first time)
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        RelatedDocument = DocumentService.GetReleaseNote(ParentDocumentId);
        InitialNumberOfItems = RelatedDocument?.Items.Count ?? 0;
        Current = AccountService.Find(Logged.GetEmail());
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
            ParentDocumentId = 0;
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Jump back to the page of the product
    /// </summary>
    public void BackToProduct()
    {
        NavigationManager.NavigateTo($"/rns?rn={RelatedDocument.ProductId}", forceLoad: true);
    }

    /// <summary>
    /// New default release note item
    /// </summary>
    /// <param name="position"></param>
    public void AddNewItem(InsertPosition position = InsertPosition.Bottom)
    {
        RelatedDocument.AddNewItem(position);
    }

    /// <summary>
    /// New default divider 
    /// </summary>
    /// <param name="position"></param>
    public void AddNewDivider(InsertPosition position = InsertPosition.Bottom)
    {
        RelatedDocument.AddNewDivider(position);
    }

    /// <summary>
    /// Remove given item
    /// </summary>
    public void RemoveItem(ReleaseNoteItem rni)
    {
        RelatedDocument.RemoveItem(rni);
    }

    /// <summary>
    /// Remove all current items
    /// </summary>
    protected async Task ClearAll()
    {
        // Ask confirmation
        var options = new ConfirmDialogOptions { IsVerticallyCentered = true };
        var shouldProceed = await Confirmation.ShowAsync(
            title: AppStrings.Label_Deleting,
            message1: $"{AppMessages.Msg_AboutToEmpty} <b class='text-primary'>{RelatedDocument.Version}</b>",
            message2: AppMessages.Prompt_ConfirmAction,
            confirmDialogOptions: options);

        if (shouldProceed)
            RelatedDocument.Items.RemoveAll(_ => true);
    }

    /// <summary>
    /// Move item one position up
    /// </summary>
    public void MoveUp(ReleaseNoteItem rni)
    {
        RelatedDocument.MoveItemUp(rni);
    }

    /// <summary>
    /// Move item one position down
    /// </summary>
    public void MoveDown(ReleaseNoteItem rni)
    {
        RelatedDocument.MoveItemDown(rni);
    }

    /// <summary>
    /// Save changes, updating/creating a releaase note document
    /// </summary>
    public async Task SaveChanges()
    {
        var author = Logged.GetEmail();
        var response = DocumentService.Update(RelatedDocument, author);
        await Statusbar.ShowAsync($"{response.Message} {response.ExtraData}", response.Success);

        // Update number of items in the view 
        InitialNumberOfItems = RelatedDocument.Items.Count;
    }

    /// <summary>
    /// Presents a modal to edit/copy the release note items as plain text
    /// </summary>
    /// <param name="rn"></param>
    protected async Task ShareAsText(ReleaseNote rn)
    {
        Share.SetHeader($"{rn.Version}", $"{rn.ReleaseDate.ToStringOrEmpty("d MMM yyyy")}");
        Share.SetTitle($"[{AppStrings.Text_ReleaseNote}]  {rn.RelatedProduct.Name}");

        // Serialize release note as plain text
        var serialized = PlainTextRenderer.Get(rn);
        Share.SetUserData(serialized);
        await Share.Window.ShowAsync();
    }
}