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


using System.Xml.Linq;
using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.DomainModel.Management;

/// <summary>
/// Roles wrapper class 
/// </summary>
public class Role
{
    private Role(string name = null)
    {
        Name = name;
    }

    /// <summary>
    /// Role name
    /// </summary>
    public string Name { get; }

    // Static names
    public const string NameOf_Contributor = "Contributor";     
    public const string NameOf_Viewer = "Viewer";     
    public const string NameOf_Owner = "Owner";
    public const string NameOf_System = "System";

    /// <summary>
    /// Specific instances
    /// </summary>
    public static Role System => new(NameOf_System);
    public static Role Owner => new(NameOf_Owner);
    public static Role Viewer => new(NameOf_Viewer);
    public static Role Contributor => new(NameOf_Contributor);

    /// <summary>
    /// All roles supported by the app
    /// </summary>
    /// <returns></returns>
    public static IList<Role> All()
    {
        return new List<Role> { Contributor, Viewer, Owner, System };
    }

    /// <summary>
    /// Custom equality operator
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Is(Role other)
    {
        return Name.Equals(other.Name);
    }

    /// <summary>
    /// Assign a numeric value to the role to determine a priority
    /// </summary>
    /// <returns></returns>
    public int Value()
    {
        return Name switch
        {
            NameOf_System => 10,
            NameOf_Owner => 7,
            NameOf_Contributor => 3,
            NameOf_Viewer => 1,
            _ => 0
        };
    }

    /// <summary>
    /// Check role type
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsRole(string roleId, Role role)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            return false;

        return roleId.EqualsAny(role.Name);  
    }

    /// <summary>
    /// Recognize the role type by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Role Parse(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return name switch
        {
            NameOf_Contributor => Contributor,
            NameOf_Viewer => Viewer,
            NameOf_Owner => Owner,
            NameOf_System => System,
            _ => null
        };
    }


    /// <summary>
    /// Display
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Name; 
    }
}
