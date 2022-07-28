﻿// <auto-generated />
using System;
using App.Infrastructures.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220723162307_Update5")]
    partial class Update5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.AppFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<string>("FileAddress")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("EntityId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("ExpertUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("SuggestedPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExpertUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.ExpertFavoriteCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("ExpertUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ExpertUserId");

                    b.ToTable("ExpertFavoriteCategories");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int?>("CustomerUserId")
                        .HasColumnType("int");

                    b.Property<int?>("FinalExpertUserId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceBasePrice")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<byte>("StatusId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.OrderFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderFiles");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.OrderStatus", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.ServiceComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CommentText")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceComments");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.ServiceFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceFiles");
                });

            modelBuilder.Entity("App.Domain.Core.User.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("PictureFileId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.AppFile", b =>
                {
                    b.HasOne("App.Domain.Core.User.Entities.AppUser", "AppUser")
                        .WithMany("AppFiles")
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Core.HomeService.Entities.Entity", "Entity")
                        .WithMany("Files")
                        .HasForeignKey("EntityId")
                        .IsRequired()
                        .HasConstraintName("FK_Files_Entities");

                    b.Navigation("AppUser");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Bid", b =>
                {
                    b.HasOne("App.Domain.Core.User.Entities.AppUser", "AppUser")
                        .WithMany("Bids")
                        .HasForeignKey("ExpertUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Core.HomeService.Entities.Order", "Order")
                        .WithMany("Bids")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Bids_Orders");

                    b.Navigation("AppUser");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.ExpertFavoriteCategory", b =>
                {
                    b.HasOne("App.Domain.Core.HomeService.Entities.Category", "Category")
                        .WithMany("ExpertFavoriteCategories")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_ExpertFavoriteCategories_Categories");

                    b.HasOne("App.Domain.Core.User.Entities.AppUser", "AppUser")
                        .WithMany("ExpertFavoriteCategories")
                        .HasForeignKey("ExpertUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Order", b =>
                {
                    b.HasOne("App.Domain.Core.User.Entities.AppUser", "AppUser")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerUserId");

                    b.HasOne("App.Domain.Core.HomeService.Entities.Service", "Service")
                        .WithMany("Orders")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Services");

                    b.HasOne("App.Domain.Core.HomeService.Entities.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_OrderStatuses");

                    b.Navigation("AppUser");

                    b.Navigation("Service");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.OrderFile", b =>
                {
                    b.HasOne("App.Domain.Core.HomeService.Entities.AppFile", "File")
                        .WithMany("OrderFiles")
                        .HasForeignKey("FileId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderFiles_Files");

                    b.HasOne("App.Domain.Core.HomeService.Entities.Order", "Order")
                        .WithMany("OrderFiles")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderFiles_Orders");

                    b.Navigation("File");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Service", b =>
                {
                    b.HasOne("App.Domain.Core.HomeService.Entities.Category", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Services_Categories");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.ServiceComment", b =>
                {
                    b.HasOne("App.Domain.Core.HomeService.Entities.Order", "Order")
                        .WithMany("ServiceComments")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceComments_Orders");

                    b.HasOne("App.Domain.Core.HomeService.Entities.Service", "Service")
                        .WithMany("ServiceComments")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceComments_Services");

                    b.Navigation("Order");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.ServiceFile", b =>
                {
                    b.HasOne("App.Domain.Core.HomeService.Entities.AppFile", "File")
                        .WithMany("ServiceFiles")
                        .HasForeignKey("FileId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceFiles_Files");

                    b.HasOne("App.Domain.Core.HomeService.Entities.Service", "Service")
                        .WithMany("ServiceFiles")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceFiles_Services");

                    b.Navigation("File");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("App.Domain.Core.User.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("App.Domain.Core.User.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Core.User.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("App.Domain.Core.User.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.AppFile", b =>
                {
                    b.Navigation("OrderFiles");

                    b.Navigation("ServiceFiles");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Category", b =>
                {
                    b.Navigation("ExpertFavoriteCategories");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Entity", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Order", b =>
                {
                    b.Navigation("Bids");

                    b.Navigation("OrderFiles");

                    b.Navigation("ServiceComments");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("App.Domain.Core.HomeService.Entities.Service", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ServiceComments");

                    b.Navigation("ServiceFiles");
                });

            modelBuilder.Entity("App.Domain.Core.User.Entities.AppUser", b =>
                {
                    b.Navigation("AppFiles");

                    b.Navigation("Bids");

                    b.Navigation("ExpertFavoriteCategories");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
