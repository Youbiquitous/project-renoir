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

using Youbiquitous.Renoir.DomainModel.Documents.Core;

namespace Youbiquitous.Renoir.DomainModel.Documents;

/// <summary>
/// Aggregation of documents and product
/// </summary>
public class DocumentContainer<TDoc, TDocItem> 
    where TDoc : CoreDocument<TDocItem> 
    where TDocItem : CoreDocumentItem, new()
{
    public DocumentContainer()
    {
        Documents = new List<TDoc>();
        Product = new Product();
    }

    /// <summary>
    /// Details of the product
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Generic list of documents
    /// </summary>
    public IEnumerable<TDoc> Documents { get; set; }
}
