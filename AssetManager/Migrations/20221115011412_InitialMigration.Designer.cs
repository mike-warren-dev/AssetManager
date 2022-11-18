﻿// <auto-generated />
using System;
using AssetManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetManager.Migrations
{
    [DbContext(typeof(AssetManagerContext))]
    [Migration("20221115011412_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetManager.Models.Asset", b =>
                {
                    b.Property<int>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssetId"));

                    b.Property<int>("AssetType")
                        .HasColumnType("int");

                    b.Property<int>("Model")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("Site")
                        .HasColumnType("int");

                    b.HasKey("AssetId");

                    b.HasIndex("PersonId");

                    b.ToTable("Asset", (string)null);
                });

            modelBuilder.Entity("AssetManager.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime?>("ApprovedDttm")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApproverId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ExternalOrderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PurchaserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReceivedDttm")
                        .HasColumnType("datetime2");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("AssetManager.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("PersonId");

                    b.ToTable("Person", (string)null);

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Email = "Melvin.Snyder@SnyderCorp.com",
                            FirstName = "Melvin",
                            LastName = "Snyder",
                            RoleId = 201
                        },
                        new
                        {
                            PersonId = 2,
                            Email = "Kevin.Wells@SnyderCorp.com",
                            FirstName = "Kevin",
                            LastName = "Wells",
                            RoleId = 200
                        },
                        new
                        {
                            PersonId = 3,
                            Email = "Martin.Hart@SnyderCorp.com",
                            FirstName = "Martin",
                            LastName = "Hart",
                            RoleId = 200
                        },
                        new
                        {
                            PersonId = 4,
                            Email = "Rose.OReilly@SnyderCorp.com",
                            FirstName = "Rose",
                            LastName = "O'Reilly",
                            RoleId = 200
                        },
                        new
                        {
                            PersonId = 5,
                            Email = "Ryan.Mills@SnyderCorp.com",
                            FirstName = "Ryan",
                            LastName = "Mills",
                            RoleId = 200
                        });
                });

            modelBuilder.Entity("AssetManager.Models.ProductOrder", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("Count")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Product")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("OrderId");

                    b.ToTable("ProductOrder");
                });

            modelBuilder.Entity("AssetManager.Models.Asset", b =>
                {
                    b.HasOne("AssetManager.Models.Person", "Person")
                        .WithMany("Assets")
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("AssetManager.Models.ProductOrder", b =>
                {
                    b.HasOne("AssetManager.Models.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("AssetManager.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("AssetManager.Models.Person", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}