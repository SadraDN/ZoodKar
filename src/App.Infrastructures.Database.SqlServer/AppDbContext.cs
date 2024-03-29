﻿using System;
using System.Collections.Generic;
using App.Domain.Core.HomeService.Entities;
using App.Domain.Core.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Infrastructures.Database.SqlServer
{
    public partial class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Entity> Entities { get; set; } = null!;
        public virtual DbSet<ExpertFavoriteService> ExpertFavoriteCategories { get; set; } = null!;
        public virtual DbSet<AppFile> Files { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderFile> OrderFiles { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceComment> ServiceComments { get; set; } = null!;
        public virtual DbSet<ServiceFile> ServiceFiles { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bids_Orders");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<ExpertFavoriteService>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ExpertFavoriteServices)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpertFavoriteCategories_Categories");
            });

            modelBuilder.Entity<AppFile>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.FileAddress).HasMaxLength(500);

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Files_Entities");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Services");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderStatuses");

                entity.HasOne(x => x.Customer)
                 .WithMany(x => x.CustomerOrders)
                 .HasForeignKey(x => x.CustomerUserId)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Expert)
                .WithMany(x => x.ExpertOrders)
                .HasForeignKey(x => x.FinalExpertUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderFile>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.OrderFiles)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderFiles_Files");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderFiles)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderFiles_Orders");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ShortDescription).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_Categories");
            });

            modelBuilder.Entity<ServiceComment>(entity =>
            {
                entity.Property(e => e.CommentText).HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ServiceComments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ServiceComments_Orders");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceComments)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ServiceComments_Services");
            });

            modelBuilder.Entity<ServiceFile>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.ServiceFiles)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceFiles_Files");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceFiles)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceFiles_Services");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
