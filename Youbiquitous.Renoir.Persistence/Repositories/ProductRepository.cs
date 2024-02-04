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
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.DomainModel;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for Product entity
/// </summary>
public partial class ProductRepository
{
    /// <summary>
    /// Retrieves the (single) user matching the email provided
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Product FindById(long id)
    {
        try
        {
            using var db = new RenoirDatabase();
            var product = db.Products
                .Include(p => p.Users)
                .SingleOrDefault(p => p.ProductId == id && !p.Deleted);
            return product;
        }
        catch (Exception ex)
        {
            var x = ex.Message;
            return null;
        }
    }

    /// <summary>
    /// Retrieves the (filtered) list of products
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static IList<Product> FindAll(string filter = "")
    {
        using var db = new RenoirDatabase();
        var query = db.Products.Where(p => !p.Deleted);
        if (!filter.IsNullOrWhitespace())
            query = query.Where(p => EF.Functions.Like(p.Name, $"%{filter}%") ||
                                     EF.Functions.Like(p.Version, $"%{filter}%"));
        return query.ToList();
    }

    /// <summary>
    /// List of bindings
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static IList<UserProductBinding> FindBindingsFor(long productId)
    {
        using var db = new RenoirDatabase();
        var bindings = db.UserProductBindings
            .Include(b => b.RelatedUser)
            .Where(b => b.ProductId == productId)
            .ToList();

        return bindings;
    }
}