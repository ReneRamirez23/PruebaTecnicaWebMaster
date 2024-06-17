using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTecnicaWebMaster.Models
{
    public partial class BD_ControlVentasContext : DbContext
    {
        public BD_ControlVentasContext()
        {
        }

        public BD_ControlVentasContext(DbContextOptions<BD_ControlVentasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SalesProduct> SalesProducts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProducts)
                    .HasName("PK__Products__0988921CE9A8295B");

                entity.Property(e => e.NameProducts).HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.IdSale)
                    .HasName("PK__Sales__A04F9B370641B110");

                entity.Property(e => e.Client).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.MailClient).HasMaxLength(50);

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("money");
            });

            modelBuilder.Entity<SalesProduct>(entity =>
            {
                entity.HasKey(e => e.IdSp)
                    .HasName("PK__SalesPro__B7701287869F1ED3");

                entity.Property(e => e.IdSp).HasColumnName("IdSP");
                entity.Property(e => e.Quantity);

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.SalesProducts)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesProducts_Products");

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.SalesProducts)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesProducts_Sales");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C9263833D472CD");

                entity.ToTable("User");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.TypeUser).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
