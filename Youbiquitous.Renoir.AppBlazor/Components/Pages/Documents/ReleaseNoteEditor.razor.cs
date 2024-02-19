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
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.Documents;

public partial class ReleaseNoteEditorPage : ViewModelBase
{
    /// <summary>
    /// Selected user and related products
    /// </summary>
    public User Current { get; set; }

    /// <summary>
    /// List of documents to present
    /// </summary>
    protected ReleaseNote RelatedDocument;
    
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
        RelatedDocument = DocumentService.Get(ParentDocumentId);
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
}