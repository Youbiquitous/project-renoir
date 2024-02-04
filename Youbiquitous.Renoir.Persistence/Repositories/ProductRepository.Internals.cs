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
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Internal methods to CRUD on the Product entity
/// </summary>
public partial class ProductRepository
{
    /// <summary>
    /// Physical insertion of new Product records
    /// </summary>
    /// <param name="db"></param>
    /// <param name="product"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    private static CommandResponse AddNewProductInternal(RenoirDatabase db, Product product, string author)
    {
        var found = db.Products.FirstOrDefault(p => p.Name == product.Name &&
                                                    p.Version == product.Version);
        if (found != null)
            return CommandResponse.Fail().AddMessage(AppMessages.Err_ProductAlreadyExists);

        // Init and save
        product.Init(author);
        db.Products.Add(product);
        return db.TrySaveChanges();
    }

    /// <summary>
    /// Edit of existing Product records
    /// </summary>
    /// <param name="db"></param>
    /// <param name="found"></param>
    /// <param name="product"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    private static CommandResponse UpdateExistingProductInternal(RenoirDatabase db, Product found, Product product, string author)
    {
        // Any other product with same name/version?
        var same = db.Products.FirstOrDefault(p => p.Name == product.Name &&
                                                   p.Version == product.Version &&
                                                   p.ProductId != found.ProductId);
        if (same != null)
            return CommandResponse.Fail().AddMessage(AppMessages.Err_ProductAlreadyExists);

        found.Import(product);
        found.Mark(author);
        return db.TrySaveChanges();
    }


    /// <summary>
    /// Physical removal of records
    /// </summary>
    /// <param name="db"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    private static CommandResponse DeleteExistingProductInternal(RenoirDatabase db, long id)
    {
        var deleted = db.Products
            .Where(p => p.ProductId == id && !p.Deleted)
            .ExecuteDelete();
        return deleted > 0
            ? CommandResponse.Ok()
            : CommandResponse.Fail();
    }

    
    /// <summary>
    /// Physical removal of user/product binding
    /// </summary>
    /// <param name="db"></param>
    /// <param name="bindingId"></param>
    /// <returns></returns>
    private static CommandResponse DeleteExistingBindingInternal(RenoirDatabase db, long bindingId)
    {
        var deleted = db.UserProductBindings
            .Where(up => up.RefId == bindingId)
            .ExecuteDelete();
        return deleted > 0
            ? CommandResponse.Ok()
            : CommandResponse.Fail();
    }
}