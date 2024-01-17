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

namespace Youbiquitous.Renoir.AppBlazor.Components.Pages.ProductArea;

public partial class ProductsPage 
{
    /// <summary>
    /// Reference to the User-Editor modal 
    /// </summary>
    protected ProductEditorPopup ProductEditor;

    /// <summary>
    /// Reference to the custom component with the table of users
    /// </summary>
    protected ProductsTable ListOfProducts;
}

