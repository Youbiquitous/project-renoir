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
    public static CommandResponse Delete(long docId)
    {
        return DocumentRepository.Delete(docId); 
    }

    /// <summary>
    /// Create a new release note document
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="version"></param>
    /// <param name="notes"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse NewReleaseNote(long productId, string version, string notes, string author)
    {
        var rn = new ReleaseNote(productId, version)
        {
            ReleaseDate = DateTime.UtcNow,
            Notes = notes,
        };
        rn.Init(author);
        rn.Mark(author);
        return DocumentRepository.Create(rn); 
    }

    /// <summary>
    /// Create a new release note document
    /// </summary>
    /// <param name="refId"></param>
    /// <param name="productId"></param>
    /// <param name="version"></param>
    /// <param name="notes"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse SaveReleaseNote(long refId, long productId, string version, string notes, string author)
    {
        var rn = new ReleaseNote(productId, version)
        {
            RefId = refId,
            ReleaseDate = DateTime.UtcNow,
            Notes = notes,
        };
//        rn.Init(author);
        rn.Mark(author);
        return DocumentRepository.Update(rn);
    }

    /// <summary>
    /// Save changes to the document
    /// </summary>
    /// <param name="rn"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse Update(ReleaseNote rn, string author)
    {
        rn.Mark(author);
        return DocumentRepository.Update(rn);
    }
}