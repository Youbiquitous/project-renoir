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
    public ProductService(RenoirSettings settings) 
        : base(settings)
    {
    }

    /// <summary>
    /// All products
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Product> Products()
    {
        return ProductRepository.FindAll();
    }

    /// <summary>
    /// One particular product with bindings
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Product Find(long id)
    {
        var prod = ProductRepository.FindById(id);
        return prod;
    }

    /// <summary>
    /// List of users enabled on given product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static IList<UserProductBinding> BindingsFor(long productId)
    {
        var bindings = ProductRepository.FindBindingsFor(productId);
        return bindings;
    }

}