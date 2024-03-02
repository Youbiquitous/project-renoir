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
    /// <summary>
    /// Retrieve all documents for user/product
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <param name="isSystem"></param>
    /// <returns></returns>
    public static DocumentContainer<Roadmap, RoadmapItem> RoadmapsFor(long userId, long productId, bool isSystem = false)
    {
        if (productId <= 0)
            return null;

        return new DocumentContainer<Roadmap, RoadmapItem>
        {
            Product = ProductRepository.FindById(productId),
            Documents = isSystem
                ? RoadmapRepository.FindAll(productId)
                : RoadmapRepository.FindAll(userId, productId)
        };
    }

    /// <summary>
    /// Retrieve the given roadmap document
    /// </summary>
    /// <param name="refId"></param>
    /// <returns></returns>
    public static Roadmap GetRoadmap(long refId)
    {
        if (refId <= 0)
            return null;

        return RoadmapRepository.FindById(refId);
    }
}