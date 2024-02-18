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


using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for document entities
/// </summary>
public partial class DocumentRepository
{
    /// <summary>
    /// Retrieves list of documents the user can access for the given product
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static IEnumerable<ReleaseNote> FindAll(long userId, long productId)
    {
        try
        {
            using var db = new RenoirDatabase();
            var product = db.Products
                .Include(p => p.Users)
                .SingleOrDefault(p => p.ProductId == productId);
            if (product == null || !product.AccessibleBy(userId))
                return new List<ReleaseNote>();

            var docs = db.ReleaseNotes
                .Include(r => r.Items)
                .Where(r => r.ProductId == productId && 
                            !r.Deleted)
                .ToList();
            return docs;
        }
        catch (Exception ex)
        {
            var x = ex.Message;
            return null;
        }
    }

    /// <summary>
    /// Retrieves the list of all documents for the product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static IEnumerable<ReleaseNote> FindAll(long productId)
    {
        try
        {
            using var db = new RenoirDatabase();
            var docs = db.ReleaseNotes
                .Include(r => r.Items)
                .Where(r => r.ProductId == productId && 
                            !r.Deleted)
                .ToList();
            return docs;
        }
        catch (Exception ex)
        {
            var x = ex.Message;
            return null;
        }
    }

    /// <summary>
    /// Remove a release note and all of its items
    /// </summary>
    /// <param name="docId"></param>
    /// <returns></returns>
    public static CommandResponse Delete(long docId)
    {
        using var db = new RenoirDatabase();
        db.ReleaseNotes
            .Where(rn => rn.RefId == docId)
            .ExecuteDelete();
        return CommandResponse.Ok();
    }

    /// <summary>
    /// Add a new release note container 
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static CommandResponse Add(ReleaseNote doc)
    {
        using var db = new RenoirDatabase();
        var found = db.ReleaseNotes.FirstOrDefault(rn => rn.Version == doc.Version);
        if (found is not null)
            return CommandResponse.Fail().AddMessage(AppMessages.Err_VersionAlreadyFound);

        db.ReleaseNotes.Add(doc);
        db.SaveChanges();
        return CommandResponse.Ok();
    }
}