using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVC_template.Data
{
    public partial class QLWebBanHangContext : DbContext
    {
        public QLWebBanHangContext()
        {
        }

        public QLWebBanHangContext(DbContextOptions<QLWebBanHangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-B0GT31Q\\MSSQLServer1; Database=QLWebBanHang;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(11);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId)
                    .HasMaxLength(50)
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("OrderID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.ProductDescription).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(50)
                    .HasColumnName("SupplierID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_Distributor");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId })
                    .HasName("PK_ShoppingCart_1");

                entity.ToTable("ShoppingCart");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_Customer");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_Product");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(50)
                    .HasColumnName("SupplierID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.SupplierName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
