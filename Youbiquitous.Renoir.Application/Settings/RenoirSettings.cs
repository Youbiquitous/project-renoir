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
/// Application-specific class holding all global settings
/// </summary>
public partial class RenoirSettings
{
    public const string AuthCookieName = "Renoir.Auth";

    public RenoirSettings()
    {
        Project = new ProjectSettings();
        DevMode = true;
    }

    /// <summary>
    /// Whether the app is intended to be in DEV mode
    /// </summary>
    public bool DevMode { get; set; }

    /// <summary>
    /// Project-specific settings: name and description
    /// </summary>
    public ProjectSettings Project { get; set; }

    /// <summary>
    /// Secret settings: database connection strings and more
    /// (if not saved locally to the laptop)
    /// </summary>
    public SecretsSettings Secrets { get; set; }


    /// <summary>
    /// Helper method to check running mode
    /// </summary>
    /// <returns></returns>
    public bool IsDevelopment()
    {
        return DevMode;
    }
}