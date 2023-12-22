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

namespace Youbiquitous.Renoir.DomainModel;

/// <summary>
/// Base class for all app entities (properties)
/// </summary>
public partial class BaseEntity
{
    protected BaseEntity()
    {
        Created = new TimeStamp();
        LastUpdated = new TimeStamp();
    }

    /// <summary>
    /// Property tracking the Deleted state
    /// </summary>
    public bool Deleted { get; private set; }

    /// <summary>
    /// Time stamp for when the record is created
    /// </summary>
    public TimeStamp Created { get; private set; }

    /// <summary>
    /// Time stamp for when the record is updated
    /// </summary>
    public TimeStamp LastUpdated { get; private set; }
}
