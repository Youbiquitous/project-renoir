///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.DomainModel;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.ProductArea;

public class ProductsPage : ViewModelBase
{
    public ProductsPage()
    {
    }

    /// <summary>
    /// List of products to render
    /// </summary>
    public IEnumerable<Product> Products { get; set; }

    /// <summary>
    /// Finalize initialization
    /// </summary>
    protected override void OnInitialized()
    {
        // Query for products
    }
}