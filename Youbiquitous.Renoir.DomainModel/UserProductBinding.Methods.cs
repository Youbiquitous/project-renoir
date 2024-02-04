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

using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.DomainModel;


/// <summary>
/// USER/PRODUCT binding (methods)
/// </summary>
public partial class UserProductBinding
{
    /// <summary>
    /// Whether the entity is in a valid state
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return UserId > 0 && 
               ProductId > 0 && 
               !string.IsNullOrWhiteSpace(RoleId);
    }

    /// <summary>
    /// Whether the role is at least as powerful as given one
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public bool AtLeast(string roleId)
    {
        var current = Role.Parse(RoleId).Value();
        var given  = Role.Parse(roleId).Value();
        return current >= given;
    }

    /// <summary>
    /// For display
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{RoleId} > {RelatedProduct}";
    }
}
