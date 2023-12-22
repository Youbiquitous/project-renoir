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

namespace Youbiquitous.Renoir.DomainModel.Management;


/// <summary>
/// User entity (methods)
/// </summary>
public partial class User
{
    /// <summary>
    /// Ensure consistency within property values
    /// </summary>
    public override void Normalize()
    {
        Email = Email?.ToLower().Trim();
        DisplayName = DisplayName?.Trim();
    }

    /// <summary>
    /// Ensure state of the object is consistent
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return Email.IsValidEmail();
    }

    /// <summary>
    /// Official display method
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return DisplayName.IsNullOrWhitespace()
            ? Email
            : DisplayName;
    }
}
