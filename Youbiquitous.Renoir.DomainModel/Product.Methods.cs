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

using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.DomainModel;


/// <summary>
/// User entity (methods)
/// </summary>
public partial class Product
{
    /// <summary>
    /// Ensure consistency within property values
    /// </summary>
    public override void Normalize()
    {
    }

    /// <summary>
    /// Ensure consistency within property values
    /// </summary>
    public override void Import(BaseEntity entity)
    {
        var other = (Product)entity;
        if (other is null)
            return;

        ProductId = other.ProductId;
        Name = other.Name;
        Version = other.Version;
    }

    /// <summary>
    /// Ensure state of the object is consistent
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return !Name.IsNullOrWhitespace();
    }

    /// <summary>
    /// Official display method
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Name} {Version}";
    }

    /// <summary>
    /// Whether the product is accessible by user to create/read notes
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool AccessibleBy(long userId)
    {
        return Users.Any(u => u.UserId == userId);
    }
}
