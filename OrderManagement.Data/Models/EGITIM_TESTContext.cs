﻿using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Data.Models
{
    public partial class EGITIM_TESTContext : DbContext
    {
        public EGITIM_TESTContext(DbContextOptions<EGITIM_TESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<CustomerData> CustomerData { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("Basket");

                entity.Property(e => e.BasketId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BasketID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("Brand");

                entity.Property(e => e.BrandId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPassword)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPhoneNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CustomerSurname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerTc)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CustomerTC")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Invoice");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderID");

                entity.ToTable("Order");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");
                
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.OrderStatus).HasColumnName("OrderStatus");

                entity.Property(e => e.OrderAddress).HasColumnType("OrderAddress");

                entity.Property(e => e.InvoiceAddress).HasColumnType("InvoiceAddress");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ProductID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
