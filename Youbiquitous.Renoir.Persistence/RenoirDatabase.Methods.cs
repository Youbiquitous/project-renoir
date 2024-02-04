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

using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Infrastructure.Security;
using Youbiquitous.Renoir.Infrastructure.Security.Password;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence;

/// <summary>
/// Helper methods for managing the MEMO database
/// </summary>
public partial class RenoirDatabase
{
    /// <summary>
    /// Pre-populate the database upon system startup
    /// </summary>
    public static void Seed(RenoirDatabase context)
    {
        // Add root and sample users
        AddSystemUser(context);

        // Add sample products
        AddSampleProducts(context);
    }

    /// <summary>
    /// Generic (but functional) save method
    /// </summary>
    /// <returns></returns>
    public CommandResponse TrySaveChanges()
    {
        try
        {
            SaveChanges();
            return CommandResponse.Ok().AddMessage(AppMessages.Msg_Done);
        }
        catch (Exception ex)
        {
            return CommandResponse.Ok()
                .AddMessage(AppMessages.Err_OperationFailed)
                .AddExtra(ex.Message);
        }
    }

    /// <summary>
    /// Create the default SYSTEM root user
    /// </summary>
    private static void AddSystemUser(RenoirDatabase context)
    {
        if (context.Users.Any())
            return;

        var root = new User(
            "system@renoir-app.net",
            PasswordServiceLocator.Get().Store("your-password"),
            Role.System.Name)
        {
            DisplayName = "Dev Team",
        };

        var own1 = new User(
            "boss1@renoir-app.net",
            PasswordServiceLocator.Get().Store("your-password"),
            Role.Owner.Name)
        {
            DisplayName = "Top Owner #1",
        };

        var own2 = new User(
            "boss2@renoir-app.net",
            PasswordServiceLocator.Get().Store("your-password"),
            Role.Owner.Name)
        {
            DisplayName = "Top Owner #2",
        };

        context.Users.AddRange(root, own1, own2);
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 1001)");
    }

    /// <summary>
    /// Create sample products
    /// </summary>
    private static void AddSampleProducts(RenoirDatabase context)
    {
        if (context.Products.Any())
            return;

        var prod1 = new Product("APOLLO", "2.0");
        var prod2 = new Product("MERCURY", "1.0");

        context.Products.AddRange(prod1, prod2);
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 1001)");
    }
}