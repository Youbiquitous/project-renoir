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

namespace Youbiquitous.Renoir.DomainModel.Management;

/// <summary>
/// Roles wrapper class 
/// </summary>
public static class Roles
{
    public const string Contributor = "Contributor";     
    public const string Viewer = "Viewer";     
    public const string Owner = "Owner";
    public const string System = "System";

    /// <summary>
    /// All roles supported by the app
    /// </summary>
    /// <returns></returns>
    public static IList<string> All()
    {
        return new List<string> { Contributor, Viewer, Owner, System };
    } 
}
