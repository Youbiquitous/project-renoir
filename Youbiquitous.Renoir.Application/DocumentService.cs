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


using Youbiquitous.Renoir.Application.Settings;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.Persistence.Repositories;

namespace Youbiquitous.Renoir.Application;

public partial class DocumentService : ApplicationServiceBase
{
    public DocumentService(RenoirSettings settings) 
        : base(settings)
    {
    }

    /// <summary>
    /// Retrieve all documents for user/product
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <param name="isSystem"></param>
    /// <returns></returns>
    public static DocumentContainer For(long userId, long productId, bool isSystem = false)
    {
        if (productId <= 0)
            return null;

        return new DocumentContainer
        {
            Product = ProductRepository.FindById(productId),
            Documents = isSystem
                ? DocumentRepository.FindAll(productId)
                : DocumentRepository.FindAll(userId, productId)
        };
    }

    /// <summary>
    /// Retrieve the given release note document
    /// </summary>
    /// <param name="refId"></param>
    /// <returns></returns>
    public static ReleaseNote Get(long refId)
    {
        if (refId <= 0)
            return null;

        return DocumentRepository.FindById(refId);
    }
}