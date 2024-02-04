///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 

using Youbiquitous.Renoir.AppBlazor.Common.Extensions;
using Youbiquitous.Renoir.AppBlazor.Models;
using Youbiquitous.Renoir.Application;
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.ProductArea;

public partial class ProductsPage : ViewModelBase
{
    /// <summary>
    /// List of products to render
    /// </summary>
    protected IEnumerable<Product> Products = new List<Product>();

    /// <summary>
    /// Finalize initialization (only first time)
    /// </summary>
    protected override void OnInitialized()
    {
        // Ensure ViewModelBase is initialized
        base.OnInitialized();
        Products = ProductService.Products();
    }
    
    /// <summary>
    /// Display the Product-Editor modal for a new product
    /// </summary>
    /// <returns></returns>
    protected async Task NewProductEditor()
    {
        ProductEditor.SetUserData(new Product());
        ProductEditor.SetTitle(AppStrings.Label_TitleNewProduct);
        ProductEditor.EditMode = false;

        await ProductEditor.Window.ShowAsync();
    }
    
    /// <summary>
    /// Finalize the modal and create a new product
    /// (Controller-level method with direct impact on UI)
    /// </summary>
    /// <param name="relatedProduct"></param>
    /// <returns></returns>
    protected async Task SaveNewOrExistingProduct(Product relatedProduct)
    {
        // Save data to the DB
        var author = Logged.GetEmail();
        var response = ProductService.SaveProduct(
            relatedProduct.ProductId,
            relatedProduct.Name,
            relatedProduct.Version,
            author);
        if (!response.Success)
        {
            await ProductEditor.Statusbar.ShowAsync(response.Message);
            return;
        }

        Refresh();
        await ProductEditor.Window.HideAsync();
    }

    /// <summary>
    /// Add a new user/product binding
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    protected async Task AddUserProductBinding(UserProductBinding binding)
    {
        var author = Logged.GetEmail();
        var response = ProductService.AddBinding(binding, author);
        if (!response.Success)
        {
            await ProductEditor.Statusbar.ShowAsync(response.Message);
            return;
        }

        // Refresh the list of active users
        OwnerEditor.AttachBindings(ProductService.BindingsFor(binding.ProductId));
    }

    /// <summary>
    /// Handler of the ProductDeleted event on the table
    /// </summary>
    /// <param name="deleted"></param>
    protected void DeleteExistingProduct(Product deleted)
    {
        // Remove given user
        var response = ProductService.Delete(deleted.ProductId);
        if (response.Success)
            Refresh();
    }

    /// <summary>
    /// Handler of the ProductEdited event on the table
    /// </summary>
    /// <param name="product"></param>
    protected async Task EditExistingProduct(Product product)
    {
        var prod = new Product();
        prod.Import(product);

        ProductEditor.SetUserData(prod);
        var title = string.Format(AppStrings.Label_TitleEditUser, prod.Name);
        ProductEditor.SetTitle(title);
        ProductEditor.EditMode = true;

        await ProductEditor.Window.ShowAsync();
    }
    
    /// <summary>
    /// Handler of the OwnerEdited event on the table
    /// (to add owners to the product)
    /// </summary>
    /// <param name="product"></param>
    protected async Task EditProductOwners(Product product)
    {
        var prod = ProductService.Find(product.ProductId);

        OwnerEditor.SetTitle(product.ToString());
        OwnerEditor.SetUserData(prod, AccountService.Accounts(), Role.All());

        await OwnerEditor.Window.ShowAsync();
    }
}