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
/// CORE-DOCUMENT-ITEM item entity (methods)
/// </summary>
public partial class CoreDocumentItem : BaseEntity
{
    /// <summary>
    /// Copy the state of another object
    /// </summary>
    public override void Import(BaseEntity entity)
    {
        var other = (CoreDocumentItem) entity;
        Category = other.Category;
        Description = other.Description;
        Order = other.Order;
    }

    /// <summary>
    /// Whether the item is unchanged
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool SameAs(CoreDocumentItem other)
    {
        return RefId == other.RefId &&
               DocumentId == other.DocumentId &&
               ProductId == other.ProductId &&
               Category == other.Category &&
               ItemType == other.ItemType &&
               Order == other.Order &&
               Description.NullOrEquals(other.Description);
    }
}
