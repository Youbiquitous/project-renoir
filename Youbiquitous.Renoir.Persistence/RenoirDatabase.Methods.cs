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
        // Add root user 
        AddSystemUser(context);
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
            return CommandResponse.Ok().AddMessage(AppStrings.Msg_Done);
        }
        catch (Exception ex)
        {
            return CommandResponse.Ok()
                .AddMessage(AppStrings.Err_OperationFailed)
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
            Roles.System)
        {
            DisplayName = "Dev Team",
        };

        context.Users.Add(root);
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 1001)");
    }
}