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
/// CORE-DOCUMENT entity (methods)
/// </summary>
public partial class CoreDocument<TDocumentItem> : BaseEntity 
    where TDocumentItem : CoreDocumentItem, new()
{
    /// <summary>
    /// Update from another instance of same class
    /// </summary>
    /// <param name="entity"></param>
    public override void Import(BaseEntity entity)
    {
        base.Import(entity);

        var doc = (CoreDocument<TDocumentItem>)entity;
        ProductId = doc.ProductId;
        ReleaseDate = doc.ReleaseDate;
        Version = doc.Version;
        Notes = doc.Notes;

        // Mark all items as deleted
        foreach (var i in Items)
            i.SoftDelete();

        foreach (var i in doc.Items
                     .OrderBy(r => r.Order))
        {
            // Complete with product ID
            i.ProductId = doc.ProductId;

            // Add new items
            if (i.RefId == 0)
            {
                i.Mark(doc.LastUpdated.By);
                Items.Add(i);
            }

            var matching = Items.FirstOrDefault(x => x.RefId == i.RefId);
            if (matching == null)
                continue;

            // Item changed
            matching.SoftUndelete();
            if (!matching.SameAs(i))
            {
                matching.Import(i);
                matching.Mark(doc.LastUpdated.By);
            }
        }

        // Delete those still marked as deleted
        Items.RemoveAll(i => i.Deleted);
    }
    

        
    /// <summary>
    /// Whether the item can be moved further up
    /// </summary>
    /// <param name="rni"></param>
    /// <returns></returns>
    public bool CanMoveItemUp(TDocumentItem rni)
    {
        return rni.Order > 1;
    }

    /// <summary>
    /// Whether the item can be moved further up
    /// </summary>
    /// <param name="rni"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool CanMoveItemDown(TDocumentItem rni)
    {
        return rni.Order < Items.Count;
    }

    /// <summary>
    /// Add a new item to given position and reorder list
    /// </summary>
    /// <param name="position"></param>
    /// <param name="actualPosition"></param>
    public void AddNewItem(InsertPosition position = InsertPosition.Bottom, int actualPosition = -1)
    {
        var item = new TDocumentItem()
        {
            ItemType = DocumentItemType.Default
        };

        if (position.IsBottom())
            Items.Add(item);
        else if (position.IsTop())
            Items.Insert(0, item);
        else if (position.IsSpecific() && actualPosition > 0)
            Items.Insert(actualPosition, item);

        RenumberListInternal();
    }

    /// <summary>
    /// Add a new divider item em to given position and reorder list
    /// </summary>
    /// <param name="position"></param>
    /// <param name="actualPosition"></param>
    public void AddNewDivider(InsertPosition position = InsertPosition.Bottom, int actualPosition = -1)
    {
        var item = new TDocumentItem
        {
            ItemType = DocumentItemType.Divider,
            Description = InternalStrings.Text_ReleaseNote_NewSection
        };

        if (position.IsBottom())
            Items.Add(item);
        else if (position.IsTop())
            Items.Insert(0, item);
        else if (position.IsSpecific() && actualPosition > 0)
            Items.Insert(actualPosition, item);

        RenumberListInternal();
    }

    /// <summary>
    /// Remove item and renumber
    /// </summary>
    /// <param name="rni"></param>
    public void RemoveItem(TDocumentItem rni)
    {
        Items.Remove(rni);
        RenumberListInternal();
    }

    /// <summary>
    /// Reorder items to move given item one position up
    /// </summary>
    /// <param name="rni"></param>
    public void MoveItemUp(TDocumentItem rni)
    {
        var oldPos = rni.Order;
        var newPos = rni.Order - 1;
        foreach (var i in Items)
        {
            if (i.Order < newPos || i.Order > oldPos)
                continue;
            if (i.Order == newPos)
            {
                i.Order = oldPos;
                continue;
            }
            if (i.Order == oldPos)
                i.Order = newPos;
        }
    }

    /// <summary>
    /// Reorder items to move given item one position down
    /// </summary>
    /// <param name="rni"></param>
    public void MoveItemDown(TDocumentItem rni)
    {
        var oldPos = rni.Order;
        var newPos = rni.Order + 1;
        foreach (var i in Items)
        {
            if (i.Order > newPos || i.Order < oldPos)
                continue;
            if (i.Order == newPos)
            {
                i.Order = oldPos;
                continue;
            }
            if (i.Order == oldPos)
                i.Order = newPos;
        }
    }



    /// <summary>
    /// Renumber items from 1 to end
    /// </summary>
    private void RenumberListInternal()
    {
        var index = 1;
        foreach(var item in Items.OrderBy(i => i.Order))
        {
            item.Order = index++;
        }
    }
}
