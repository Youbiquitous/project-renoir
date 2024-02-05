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
using Youbiquitous.Renoir.Application.Settings;
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Persistence.Repositories;

namespace Youbiquitous.Renoir.Application;

public partial class ProductService : ApplicationServiceBase
{
    /// <summary>
    /// Edit/create a new product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="version"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse SaveProduct(long id, string name, string version, string author)
    {
        var product = new Product(id, name, version);
        var response = ProductRepository.Upsert(product, author);
        return response;
    }

    /// <summary>
    /// Add a new user/product binding
    /// </summary>
    /// <param name="binding"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse AddBinding(UserProductBinding binding, string author)
    {
        var response = ProductRepository.AddBinding(binding, author);
        return response;
    }

    /// <summary>
    /// Delete an existing product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static CommandResponse Delete(long id)
    {
        return ProductRepository.Delete(id); 
    }

    /// <summary>
    /// Remove binding
    /// </summary>
    /// <param name="binding"></param>
    public static void RemoveBinding(UserProductBinding binding)
    {
        ProductRepository.RemoveBinding(binding);
    }
}