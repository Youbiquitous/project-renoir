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
/// Categories for release-note items
/// </summary>
public enum ItemCategory
{
    None = 0,
    Bug = 1,
    Feature = 2,
    Internal = 3
}
