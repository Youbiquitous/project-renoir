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
/// Base class for all app entities (methods)
/// </summary>
public partial class BaseEntity
{
    /// <summary>
    /// Set the created timestamp
    /// </summary>
    /// <param name="author"></param>
    public virtual void Init(string author)
    {
        Created.Mark(author);
        LastUpdated.Mark(author);
    }

    /// <summary>
    /// Set the last-updated timestamp
    /// </summary>
    /// <param name="author"></param>
    public void Mark(string author)
    {
        LastUpdated.Mark(author);
    }

    /// <summary>
    /// Whether the object is in valid state (whatever that means)
    /// </summary>
    /// <returns></returns>
    public virtual bool IsValid()
    {
        return true;
    }

    /// <summary>
    /// Ensure that properties have "normalized" values (ie, trimmed), whatever that means
    /// </summary>
    public virtual void Normalize()
    {
    }

    /// <summary>
    /// Copy the state of another object
    /// </summary>
    public virtual void Import(BaseEntity entity)
    {
        Deleted = entity.Deleted;
    }

    /// <summary>
    /// Performs soft deletion of the instance
    /// </summary>
    /// <returns></returns>
    public void SoftDelete()
    {
        Deleted = true;
    }

    /// <summary>
    /// Performs soft undelete of the instance
    /// </summary>
    /// <returns></returns>
    public void SoftUndelete()
    {
        Deleted = false;
    }
}
