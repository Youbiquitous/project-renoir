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
using System;
using System.Transactions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence;

public partial class RenoirDatabase : DbContext
{
    public static string ConnectionString = "";

    /// <summary>
    /// Overridable method
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    // Tables
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserProductBinding> UserProductBindings { get; set; }
    public DbSet<ReleaseNote> ReleaseNotes { get; set; }
    public DbSet<ReleaseNoteItem> ReleaseNoteItems { get; set; }
}

/// <summary>
/// Helper extension methods
/// </summary>
public static class RenoirDatabaseExtensions
{
    /// <summary>
    /// Try/catch around SaveChanges
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static CommandResponse TrySaveChanges(this RenoirDatabase db)
    {
        try
        {
            db.SaveChanges();
            return CommandResponse.Ok().AddMessage(AppMessages.Msg_SuccessfullyDone);
        }
        catch (Exception ex)
        {
            var extra = $"{ex.Message}|{ex.InnerException?.Message}".Trim('|');
            return CommandResponse.Fail()
                .AddMessage(AppMessages.Err_OperationFailed)
                .AddExtra(extra);
        }
    }

    /// <summary>
    /// Try/catch around SaveChanges
    /// </summary>
    /// <param name="db"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static CommandResponse TrySaveChanges(this RenoirDatabase db, TransactionScope scope)
    {
        try
        {
            db.SaveChanges();
            scope.Complete();
            return CommandResponse.Ok().AddMessage(AppMessages.Msg_SuccessfullyDone);
        }
        catch (Exception ex)
        {
            var extra = $"{ex.Message}|{ex.InnerException?.Message}".Trim('|');
            return CommandResponse.Fail()
                .AddMessage(AppMessages.Err_OperationFailed)
                .AddExtra(extra);
        }
    }
}