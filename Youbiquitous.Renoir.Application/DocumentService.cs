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
using Youbiquitous.Renoir.DomainModel;
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
    /// <returns></returns>
    public static IEnumerable<ReleaseNote> For(long userId, long productId)
    {
        if (productId <= 0)
            return null;

        var rnd = new Random();
        return new List<ReleaseNote>()
        {
            new() {Version=$"{productId} v{rnd.Next(1, 10)}.{rnd.Next(1, 100)}"},
            new() {Version=$"{productId} v{rnd.Next(1, 10)}.{rnd.Next(1, 100)}"},
            new() {Version=$"{productId} v{rnd.Next(1, 10)}.{rnd.Next(1, 100)}"},
        };
    }
}