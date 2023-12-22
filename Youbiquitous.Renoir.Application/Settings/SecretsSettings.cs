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
/// Container of connection strings
/// </summary>
public class SecretsSettings
{
    public SecretsSettings()
    {
        RenoirDatabase = new StorageDescriptor();
        RenoirBlob = new StorageDescriptor();
    }

    /// <summary>
    /// Application database
    /// </summary>
    public StorageDescriptor RenoirDatabase { get; set; }

    /// <summary>
    /// Application blob storage (if any)
    /// </summary>
    public StorageDescriptor RenoirBlob { get; set; }
}



/// <summary>
/// Helper class descriptor
/// </summary>
public class StorageDescriptor
{
    /// <summary>
    /// Current connection string to use (live/local/more)
    /// </summary>
    public string Selector { get; set; }

    /// <summary>
    /// Production connection string
    /// </summary>
    public string Live { get; set; }

    /// <summary>
    /// Local connection string
    /// </summary>
    public string Local { get; set; }

    /// <summary>
    /// Return the currently selected connection string
    /// </summary>
    /// <returns></returns>
    public string Get()
    {
        return Selector.ToLower() switch
        {
            "live" => Live,
            _ => Local
        };
    }
}