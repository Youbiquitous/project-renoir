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


namespace Youbiquitous.Renoir.Persistence;

/// <summary>
/// Database console
/// </summary>
public class RenoirDatabaseInitializer
{
    /// <summary>
    /// Database initializer
    /// </summary>
    /// <param name="connString"></param>
    public void Initialize(string connString)
    {
        RenoirDatabase.ConnectionString = connString;

        var db1 = new RenoirDatabase();
        if (db1.Database.EnsureCreated())
            Seed(db1);
    }

    /// <summary>
    /// Pre-populate the database upon system startup
    /// </summary>
    /// <param name="context"></param>
    private static void Seed(RenoirDatabase context)
    {
        RenoirDatabase.Seed(context);
    }
}