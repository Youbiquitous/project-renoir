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
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Management;

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
        #region BINDINGS
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
        #endregion

        ////////////////////////////////////////////////////////
        //  RELEASE NOTE(s)
        // 
        #region RELEASE NOTES
        modelBuilder.Entity<ReleaseNote>()
            .Property(p => p.RefId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<ReleaseNote>()
            .HasKey(rn => rn.RefId);
        modelBuilder.Entity<ReleaseNote>(p =>
        {
            p.ComplexProperty(r => r.Created).IsRequired();
            p.ComplexProperty(r => r.LastUpdated).IsRequired();
        });

        modelBuilder.Entity<ReleaseNote>()
            .HasOne(rn => rn.RelatedProduct)
            .WithMany(p => p.ReleaseNotes)
            .HasForeignKey(rn => new {rn.ProductId})
            .OnDelete(DeleteBehavior.Cascade);

        // Release Note items
        modelBuilder.Entity<ReleaseNoteItem>()
            .Property(p => p.RefId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<ReleaseNoteItem>()
            .HasKey(rn => rn.RefId);
        modelBuilder.Entity<ReleaseNoteItem>(p =>
        {
            p.ComplexProperty(r => r.Created).IsRequired();
            p.ComplexProperty(r => r.LastUpdated).IsRequired();
        });

        modelBuilder.Entity<ReleaseNoteItem>()
            .HasOne(rni => rni.RelatedDocument)
            .WithMany(rn => rn.Items)
            .HasForeignKey(rn => new {rn.DocumentId})
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        ////////////////////////////////////////////////////////
        //  ROADMAP(s)
        // 
        #region ROADMAPS
        modelBuilder.Entity<Roadmap>()
            .Property(p => p.RefId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Roadmap>()
            .HasKey(rm => rm.RefId);
        modelBuilder.Entity<Roadmap>(p =>
        {
            p.ComplexProperty(r => r.Created).IsRequired();
            p.ComplexProperty(r => r.LastUpdated).IsRequired();
        });

        modelBuilder.Entity<Roadmap>()
            .HasOne(rm => rm.RelatedProduct)
            .WithMany(p => p.Roadmaps)
            .HasForeignKey(rm => new {rm.ProductId})
            .OnDelete(DeleteBehavior.Cascade);

        // Roadmap items
        modelBuilder.Entity<RoadmapItem>()
            .Property(p => p.RefId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<RoadmapItem>()
            .HasKey(rm => rm.RefId);
        modelBuilder.Entity<RoadmapItem>(p =>
        {
            p.ComplexProperty(r => r.Created).IsRequired();
            p.ComplexProperty(r => r.LastUpdated).IsRequired();
        });

        modelBuilder.Entity<RoadmapItem>()
            .HasOne(rmi => rmi.RelatedDocument)
            .WithMany(rm => rm.Items)
            .HasForeignKey(rm => new {rm.DocumentId})
            .OnDelete(DeleteBehavior.Cascade);
        #endregion
    }
}