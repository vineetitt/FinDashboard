﻿// <auto-generated />
using System;
using FinDashboard.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinDashboard.API.Migrations
{
    [DbContext(typeof(FinDashboardDbContext))]
    [Migration("20241120052708_AddedNewModelsAndChangedOlderOnes")]
    partial class AddedNewModelsAndChangedOlderOnes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Holding", b =>
                {
                    b.Property<int>("HoldingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HoldingId"));

                    b.Property<int>("PortfolioID")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalInvested")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("HoldingId");

                    b.HasIndex("PortfolioID");

                    b.HasIndex("StockId");

                    b.ToTable("Holding");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Portfolio", b =>
                {
                    b.Property<int>("PortfolioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PortfolioId"));

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InvestedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PortfolioId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockID"));

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HighPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LowPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpenPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PortfolioId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockID");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Holding", b =>
                {
                    b.HasOne("FinDashboard.API.Models.Domain.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinDashboard.API.Models.Domain.Stock", "Stock")
                        .WithMany("Holdings")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Portfolio", b =>
                {
                    b.HasOne("FinDashboard.API.Models.Domain.User", "User")
                        .WithOne("Portfolio")
                        .HasForeignKey("FinDashboard.API.Models.Domain.Portfolio", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Stock", b =>
                {
                    b.HasOne("FinDashboard.API.Models.Domain.Portfolio", null)
                        .WithMany("Assets")
                        .HasForeignKey("PortfolioId");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Portfolio", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.Stock", b =>
                {
                    b.Navigation("Holdings");
                });

            modelBuilder.Entity("FinDashboard.API.Models.Domain.User", b =>
                {
                    b.Navigation("Portfolio")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}