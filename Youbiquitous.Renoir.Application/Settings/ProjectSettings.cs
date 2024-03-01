//////////////////////////////////////////////////////////////////////////
//
// Building Enterprise ASP.NET Core 6 Blazor Applications
// Source code
//
// Dino Esposito 
// 2023
// 

namespace Youbiquitous.Renoir.Application.Settings;

/// <summary>
/// Container of project identity parameters
/// </summary>
public class ProjectSettings
{
    /// <summary>
    /// App name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// App description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// App version
    /// </summary>
    public string Version { get; set; }
    
    /// <summary>
    /// App version date
    /// </summary>
    public string ReleaseDate { get; set; }

    /// <summary>
    /// Copyright note
    /// </summary>
    public string Copyright { get; set; }
    
    /// <summary>
    /// Optional attribution for free resources
    /// </summary>
    public string Attribution { get; set; }
}