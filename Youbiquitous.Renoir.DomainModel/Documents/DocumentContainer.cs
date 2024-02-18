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
/// View model for the product/doc page
/// </summary>
public partial class DocumentContainer  
{
    public DocumentContainer()
    {
        Documents = new List<ReleaseNote>();
        Product = new Product();
    }

    /// <summary>
    /// Details of the product
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// List of documents
    /// </summary>
    public IEnumerable<ReleaseNote> Documents { get; set; }
}
