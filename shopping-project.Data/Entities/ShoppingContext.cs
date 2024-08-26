using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

public partial class ShoppingContext : DbContext
{
    public ShoppingContext()
    {
    }

    public ShoppingContext(DbContextOptions<ShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAddress> TblAddresses { get; set; }

    public virtual DbSet<TblCart> TblCarts { get; set; }

    public virtual DbSet<TblCartItem> TblCartItems { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblImage> TblImages { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblRating> TblRatings { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblVerifiedUser> TblVerifiedUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Shopping-ProjectDb;Persist Security Info=False;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAddress>(entity =>
        {
            entity.HasOne(d => d.User).WithOne(p => p.TblAddress).HasConstraintName("FK_Tbl Address_TblUser");
        });

        modelBuilder.Entity<TblCart>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.TblCarts).HasConstraintName("FK_TblCart_TblUser");
        });

        modelBuilder.Entity<TblCartItem>(entity =>
        {
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Cart).WithMany(p => p.TblCartItems).HasConstraintName("FK_TblCartItem_TblCart");

            entity.HasOne(d => d.Product).WithMany(p => p.TblCartItems).HasConstraintName("FK_TblCartItem_TblProduct");
        });

        modelBuilder.Entity<TblImage>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.TblImages).HasConstraintName("FK_TblImage_TblProduct");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.TblProducts)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TblProduct_TblCategory");
        });

        modelBuilder.Entity<TblRating>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.TblRatings).HasConstraintName("FK_TblRating_TblProduct");

            entity.HasOne(d => d.User).WithMany(p => p.TblRatings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblRating_TblUser");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.Property(e => e.RoleId).HasDefaultValue(1);

            entity.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUser_TblRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
