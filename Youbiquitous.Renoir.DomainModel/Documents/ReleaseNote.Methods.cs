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


namespace Youbiquitous.Renoir.DomainModel.Documents;

/// <summary>
/// RELEASE-NOTE entity (properties)
/// </summary>
public partial class ReleaseNote : BaseEntity
{
    /// <summary>
    /// Update from another instance 
    /// </summary>
    /// <param name="entity"></param>
    public override void Import(BaseEntity entity)
    {
        base.Import(entity);

        var doc = (ReleaseNote)entity;
        ProductId = doc.ProductId;
        ReleaseDate = doc.ReleaseDate;
        Version = doc.Version;

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
    /// Add a new item to given position and reorder list
    /// </summary>
    /// <param name="position"></param>
    /// <param name="actualPosition"></param>
    public void AddNewItem(InsertPosition position = InsertPosition.Bottom, int actualPosition = -1)
    {
        var item = ReleaseNoteItem.Default();
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
    public void RemoveItem(ReleaseNoteItem rni)
    {
        Items.Remove(rni);
        RenumberListInternal();
    }

    /// <summary>
    /// Reorder items to move given item one position up
    /// </summary>
    /// <param name="rni"></param>
    public void MoveItemUp(ReleaseNoteItem rni)
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
    public void MoveItemDown(ReleaseNoteItem rni)
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
        for (var i = 0; i < Items.Count; i++)
        {
            Items[i].Order = i + 1;
        }
    }
}
