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

using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for Product entity
/// </summary>
public partial class ProductRepository
{
    /// <summary>
    /// Edit/add a new product
    /// </summary>
    /// <param name="product"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse Upsert(Product product, string author)
    {
        using var db = new RenoirDatabase();
        var found = db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

        var response = found is null
            ? AddNewProductInternal(db, product, author)
            : UpdateExistingProductInternal(db, found, product, author);

        return response;
    }

    /// <summary>
    /// Add a new binding
    /// </summary>
    /// <param name="binding"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse AddBinding(UserProductBinding binding, string author)
    {
        using var db = new RenoirDatabase();
        var found = db.UserProductBindings
            .FirstOrDefault(up => up.UserId == binding.UserId &&
                                  up.ProductId == binding.ProductId);

        // If user has already a binding => update; then, add
        binding.Mark(author);
        if (found is null)
        {
            db.UserProductBindings.Add(binding);
        }
        else
        {
            found.RoleId = binding.RoleId;
        }

        return db.TrySaveChanges();
    }

    /// <summary>
    /// Remove binding
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    public static CommandResponse RemoveBinding(UserProductBinding binding)
    {
        using var db = new RenoirDatabase();
        return DeleteExistingBindingInternal(db, binding.RefId);
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static CommandResponse Delete(long id)
    {
        using var db = new RenoirDatabase();
        return DeleteExistingProductInternal(db, id);
    }
}