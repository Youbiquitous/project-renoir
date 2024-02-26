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
/// RELEASE-NOTE entity (properties)
/// </summary>
public partial class ReleaseNote : CoreDocument<ReleaseNoteItem>
{
    /// <summary>
    /// Ctor, mostly needed for EF and to save us an entire layer of DTOs
    /// </summary>
    public ReleaseNote() 
    {
    }
    public ReleaseNote(long productId, string version)  
    {
        ProductId = productId;
        Version = version;
    }
}
