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
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Management;
using Product = Youbiquitous.Renoir.DomainModel.Product;

namespace Youbiquitous.Renoir.Persistence;

/// <summary>
/// MODELING attributes
/// </summary>
public partial class RenoirDatabase 
{
    /// <summary>
    /// Overridable model builder
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ////////////////////////////////////////////////////////
        //  USER
        // 
        #region USER
        modelBuilder.Entity<User>()
            .Property(u => u.UserId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<User>()
            .HasKey(u => new { u.UserId });
        modelBuilder.Entity<User>(u =>
        {
            u.ComplexProperty(r => r.Created).IsRequired();
            u.ComplexProperty(r => r.LastUpdated).IsRequired();
        });
        #endregion

        ////////////////////////////////////////////////////////
        //  PRODUCT(s)
        // 
        #region PRODUCT
        modelBuilder.Entity<Product>()
            .Property(p => p.ProductId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Product>()
            .HasKey(p => new { p.ProductId });
        modelBuilder.Entity<Product>(p =>
        {
            p.ComplexProperty(r => r.Created).IsRequired();
            p.ComplexProperty(r => r.LastUpdated).IsRequired();
        });
        #endregion

        ////////////////////////////////////////////////////////
        //  USER/PRODUCT BINDING(s)
        // 
        modelBuilder.Entity<UserProductBinding>()
            .HasKey(up => up.RefId);
        modelBuilder.Entity<UserProductBinding>(p =>
        {
            p.ComplexProperty(r => r.Created).IsRequired();
            p.ComplexProperty(r => r.LastUpdated).IsRequired();
        });

        // Keys to user/product mapping
        modelBuilder.Entity<UserProductBinding>()
            .HasOne(up => up.RelatedUser)
            .WithMany(u => u.Products)
            .HasForeignKey(u => u.UserId);

        // Keys to tournament/umpire mapping
        modelBuilder.Entity<UserProductBinding>()
            .HasOne(up => up.RelatedProduct)
            .WithMany(p => p.Users)
            .HasForeignKey(p => p.ProductId);
    }
}