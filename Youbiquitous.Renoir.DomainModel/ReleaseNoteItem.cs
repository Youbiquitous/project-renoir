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

using System.ComponentModel.DataAnnotations;

namespace Youbiquitous.Renoir.DomainModel;

/// <summary>
/// RELEASE-NOTE item entity (properties)
/// </summary>
public partial class ReleaseNoteItem : BaseEntity
{
    /// <summary>
    /// Ctor, mostly needed for EF and to save us an entire layer of DTOs
    /// </summary>
    public ReleaseNoteItem()
    {
    }

    public ItemCategory Category { get; set; }

    [MaxLength(140)]
    public string Description { get; set; }

    [MaxLength(30)]
    public string Eta { get; set; }
}
