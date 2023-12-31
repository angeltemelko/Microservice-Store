﻿// <auto-generated />
using Microservices.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Microservices.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230626191622_ProductDatabase")]
    partial class ProductDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microservices.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dsecprition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Category 1",
                            Dsecprition = "Description 1",
                            ImageUrl = "https://picsum.photos/200/300?random=1",
                            Name = "Product 1",
                            Price = 100.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Category 2",
                            Dsecprition = "Description 2",
                            ImageUrl = "https://picsum.photos/200/300?random=2",
                            Name = "Product 2",
                            Price = 200.0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Category 3",
                            Dsecprition = "Description 3",
                            ImageUrl = "https://picsum.photos/200/300?random=3",
                            Name = "Product 3",
                            Price = 300.0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Category 4",
                            Dsecprition = "Description 4",
                            ImageUrl = "https://picsum.photos/200/300?random=4",
                            Name = "Product 4",
                            Price = 400.0
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryName = "Category 5",
                            Dsecprition = "Description 5",
                            ImageUrl = "https://picsum.photos/200/300?random=5",
                            Name = "Product 5",
                            Price = 500.0
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryName = "Category 6",
                            Dsecprition = "Description 6",
                            ImageUrl = "https://picsum.photos/200/300?random=6",
                            Name = "Product 6",
                            Price = 600.0
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryName = "Category 7",
                            Dsecprition = "Description 7",
                            ImageUrl = "https://picsum.photos/200/300?random=7",
                            Name = "Product 7",
                            Price = 700.0
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryName = "Category 8",
                            Dsecprition = "Description 8",
                            ImageUrl = "https://picsum.photos/200/300?random=8",
                            Name = "Product 8",
                            Price = 800.0
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryName = "Category 9",
                            Dsecprition = "Description 9",
                            ImageUrl = "https://picsum.photos/200/300?random=9",
                            Name = "Product 9",
                            Price = 900.0
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryName = "Category 10",
                            Dsecprition = "Description 10",
                            ImageUrl = "https://picsum.photos/200/300?random=10",
                            Name = "Product 10",
                            Price = 1000.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
