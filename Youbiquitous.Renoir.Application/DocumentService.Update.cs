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

public partial class DocumentService : ApplicationServiceBase
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
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse NewReleaseNote(long productId, string version, string author)
    {
        var rn = new ReleaseNote(productId, version)
        {
            ReleaseDate = DateTime.UtcNow
        };
        rn.Created.Mark(author);
        rn.LastUpdated.Mark(author);
        return DocumentRepository.Add(rn); 
    }
}