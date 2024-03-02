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
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.Persistence.Repositories;

namespace Youbiquitous.Renoir.Application;

public partial class DocumentService 
{
    /// <summary>
    /// Delete an existing document
    /// </summary>
    /// <param name="docId"></param>
    /// <returns></returns>
    public static CommandResponse DeleteRoadmap(long docId)
    {
        return RoadmapRepository.Delete(docId); 
    }

    /// <summary>
    /// Create a new roadmap document
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="version"></param>
    /// <param name="date"></param>
    /// <param name="notes"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse NewRoadmap(long productId, string version, DateTime? date, string notes, string author)
    {
        var rn = new Roadmap(productId, version)
        {
            ReleaseDate = date.GetValueOrDefault(DateTime.UtcNow.Date),
            Notes = notes,
        };

        rn.Init(author);
        rn.Mark(author);
        return RoadmapRepository.Create(rn); 
    }

    /// <summary>
    /// Create a new release note document
    /// </summary>
    /// <param name="refId"></param>
    /// <param name="productId"></param>
    /// <param name="version"></param>
    /// <param name="date"></param>
    /// <param name="notes"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse SaveRoadmap(long refId, long productId, string version, DateTime? date, string notes, string author)
    {
        var rmp = new Roadmap(productId, version)
        {
            RefId = refId,
            ReleaseDate = date.GetValueOrDefault(DateTime.UtcNow.Date),
            Notes = notes,
        };

        rmp.Mark(author);
        return RoadmapRepository.Update(rmp);
    }

    /// <summary>
    /// Save changes to the document
    /// </summary>
    /// <param name="rmp"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse Update(Roadmap rmp, string author)
    {
        rmp.Mark(author);
        return RoadmapRepository.Update(rmp);
    }
}