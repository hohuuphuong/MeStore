﻿// <auto-generated />
using System;
using MeShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeShop.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240801163413_modifyImage")]
    partial class modifyImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeShop.Models.AdminUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admin_Users");
                });

            modelBuilder.Entity("MeShop.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("MeShop.Models.Cart_Item", b =>
                {
                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<int>("Cart_Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsSelection")
                        .HasColumnType("bit");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Product_Id", "Cart_Id");

                    b.HasIndex("Cart_Id");

                    b.ToTable("Cart_Items");
                });

            modelBuilder.Entity("MeShop.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount_Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("MeShop.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Product_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("MeShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deleted_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Discount_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProductCategory_Id")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Discount_Id");

                    b.HasIndex("ProductCategory_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MeShop.Models.Product_Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deleted_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product_Categories");
                });

            modelBuilder.Entity("MeShop.Models.Product_Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deleted_At")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products_Inventory");
                });

            modelBuilder.Entity("MeShop.Models.Product_Size", b =>
                {
                    b.Property<int>("Size_Id")
                        .HasColumnType("int");

                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductInventory_Id")
                        .HasColumnType("int");

                    b.HasKey("Size_Id", "Product_Id");

                    b.HasIndex("ProductInventory_Id")
                        .IsUnique();

                    b.HasIndex("Product_Id");

                    b.ToTable("Products_Size");
                });

            modelBuilder.Entity("MeShop.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("MeShop.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cart_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Cart_Id")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MeShop.Models.User_Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address_City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address_Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address_District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address_Ward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("User_Addresses");
                });

            modelBuilder.Entity("MeShop.Models.Cart_Item", b =>
                {
                    b.HasOne("MeShop.Models.Cart", "Cart")
                        .WithMany("Cart_Items")
                        .HasForeignKey("Cart_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeShop.Models.Product", "Product")
                        .WithMany("Cart_Items")
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MeShop.Models.Image", b =>
                {
                    b.HasOne("MeShop.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("Product_Id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MeShop.Models.Product", b =>
                {
                    b.HasOne("MeShop.Models.Discount", "Discount")
                        .WithMany("Products")
                        .HasForeignKey("Discount_Id");

                    b.HasOne("MeShop.Models.Product_Category", "Product_Category")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategory_Id");

                    b.Navigation("Discount");

                    b.Navigation("Product_Category");
                });

            modelBuilder.Entity("MeShop.Models.Product_Size", b =>
                {
                    b.HasOne("MeShop.Models.Product_Inventory", "Product_Inventory")
                        .WithOne("Product_Size")
                        .HasForeignKey("MeShop.Models.Product_Size", "ProductInventory_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeShop.Models.Product", "Product")
                        .WithMany("Product_Sizes")
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeShop.Models.Size", "Size")
                        .WithMany("Product_Sizes")
                        .HasForeignKey("Size_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Product_Inventory");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("MeShop.Models.User", b =>
                {
                    b.HasOne("MeShop.Models.Cart", "Cart")
                        .WithOne("User")
                        .HasForeignKey("MeShop.Models.User", "Cart_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("MeShop.Models.User_Address", b =>
                {
                    b.HasOne("MeShop.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("User_Id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeShop.Models.Cart", b =>
                {
                    b.Navigation("Cart_Items");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeShop.Models.Discount", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MeShop.Models.Product", b =>
                {
                    b.Navigation("Cart_Items");

                    b.Navigation("Images");

                    b.Navigation("Product_Sizes");
                });

            modelBuilder.Entity("MeShop.Models.Product_Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MeShop.Models.Product_Inventory", b =>
                {
                    b.Navigation("Product_Size")
                        .IsRequired();
                });

            modelBuilder.Entity("MeShop.Models.Size", b =>
                {
                    b.Navigation("Product_Sizes");
                });

            modelBuilder.Entity("MeShop.Models.User", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
