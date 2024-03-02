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

using System.ComponentModel.DataAnnotations;
using Youbiquitous.Renoir.DomainModel.Documents.Core;
using Youbiquitous.Renoir.DomainModel.Utils;

namespace Youbiquitous.Renoir.DomainModel.Documents;

/// <summary>
/// RELEASE-NOTE item entity (properties)
/// </summary>
public partial class ReleaseNoteItem : CoreDocumentItem
{
    /// <summary>
    /// Ctor, mostly needed for EF and to save us an entire layer of DTOs
    /// </summary>
    public ReleaseNoteItem()
    {
        ItemType = DocumentItemType.Default;
        Order = 1000;       // Ensure it goes to the bottom when added
    }

    /// <summary>
    /// Reference to parent object
    /// </summary>
    public ReleaseNote RelatedDocument { get; set; }

    /// <summary>
    /// Free text
    /// </summary>
    [MaxLength(80)]
    public string Status { get; set; }

    /// <summary>
    /// Whether the item is unchanged
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool SameAs(CoreDocumentItem obj)
    {
        var other = (ReleaseNoteItem)obj;

        return RefId == other.RefId &&
               DocumentId == other.DocumentId &&
               ProductId == other.ProductId &&
               Category == other.Category &&
               ItemType == other.ItemType &&
               Order == other.Order &&
               Description.NullOrEquals(other.Description) &&
               Status.NullOrEquals(other.Status);
    }

    /// <summary>
    /// Copy the state of another object
    /// </summary>
    public override void Import(BaseEntity entity)
    {
        base.Import(entity);

        var other = (ReleaseNoteItem)entity;
        Category = other.Category;
        Status = other.Status;
        Description = other.Description;
        Order = other.Order;
    }
}
