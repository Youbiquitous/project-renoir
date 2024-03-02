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
/// ROADMAP entity (properties)
/// </summary>
public partial class Roadmap : CoreDocument<RoadmapItem>
{
    /// <summary>
    /// Ctor, mostly needed for EF and to save us an entire layer of DTOs
    /// </summary>
    public Roadmap() 
    {
    }
    public Roadmap(long productId, string version)  
    {
        ProductId = productId;
        Version = version;
    }
}
