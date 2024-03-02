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
/// ROADMAP item entity (properties)
/// </summary>
public partial class RoadmapItem : CoreDocumentItem
{
    /// <summary>
    /// Ctor, mostly needed for EF and to save us an entire layer of DTOs
    /// </summary>
    public RoadmapItem()
    {
        ItemType = DocumentItemType.Default;
        Order = 1000;       // Ensure it goes to the bottom when added
    }

    /// <summary>
    /// Reference to parent object
    /// </summary>
    public Roadmap RelatedDocument { get; set; }

    /// <summary>
    /// Estimated release date
    /// </summary>
    [MaxLength(80)]
    public string Eta { get; set; }

    /// <summary>
    /// Whether the item is unchanged
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool SameAs(CoreDocumentItem obj)
    {
        var other = (RoadmapItem)obj;

        return RefId == other.RefId &&
               DocumentId == other.DocumentId &&
               ProductId == other.ProductId &&
               Category == other.Category &&
               ItemType == other.ItemType &&
               Order == other.Order &&
               Description.NullOrEquals(other.Description) &&
               Eta.NullOrEquals(other.Eta);
    }

    /// <summary>
    /// Copy the state of another object
    /// </summary>
    public override void Import(BaseEntity entity)
    {
        base.Import(entity);

        var other = (RoadmapItem)entity;
        Category = other.Category;
        Eta = other.Eta;
        Description = other.Description;
        Order = other.Order;
    }
}
