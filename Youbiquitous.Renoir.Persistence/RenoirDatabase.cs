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
using Youbiquitous.Renoir.DomainModel.Management;
using Product = Youbiquitous.Renoir.DomainModel.Product;

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
}