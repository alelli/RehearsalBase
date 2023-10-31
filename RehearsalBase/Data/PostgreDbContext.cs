using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RehearsalBase.Data
{
    public partial class PostgreDbContext : DbContext
    {
        public PostgreDbContext()
        {
        }

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Rehearsal> Rehearsals { get; set; }

        public virtual DbSet<RehearsalCategory> RehearsalCategories { get; set; }

        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public virtual DbSet<ValidSubscription> ValidSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerEmail, "customers_customer_email_key").IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(50)
                    .HasColumnName("customer_email");
                entity.Property(e => e.CustomerName)
                    .HasMaxLength(15)
                    .HasColumnName("customer_name");
                entity.Property(e => e.CustomerPassword)
                    .HasMaxLength(255)
                    .HasColumnName("customer_password");
            });

            modelBuilder.Entity<Rehearsal>(entity =>
            {
                entity.HasKey(e => e.RehearsalId).HasName("rehearsals_pkey");

                entity.ToTable("rehearsals");

                entity.Property(e => e.RehearsalId).HasColumnName("rehearsal_id");
                entity.Property(e => e.Category).HasColumnName("category");
                entity.Property(e => e.Customer).HasColumnName("customer");
                entity.Property(e => e.RehearsalDate).HasColumnName("rehearsal_date");
                entity.Property(e => e.RehearsalEnd).HasColumnName("rehearsal_end");
                entity.Property(e => e.RehearsalStart).HasColumnName("rehearsal_start");

                entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Rehearsals)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rehearsals_category_fkey");

                entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Rehearsals)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("rehearsals_customer_fkey");
            });

            modelBuilder.Entity<RehearsalCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("rehearsal_categories_pkey");

                entity.ToTable("rehearsal_categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<SubscriptionType>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId).HasName("subscription_types_pkey");

                entity.ToTable("subscription_types");

                entity.HasIndex(e => e.SubscriptionId, "subscription_types_subscription_id_key").IsUnique();

                entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
                entity.Property(e => e.Category).HasColumnName("category");
                entity.Property(e => e.Hours).HasColumnName("hours");
                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.SubscriptionTypes)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subscription_types_category_fkey");
            });

            modelBuilder.Entity<ValidSubscription>(entity =>
            {
                entity.HasKey(e => e.ValidSubId).HasName("valid_subscriptions_pkey");

                entity.ToTable("valid_subscriptions");

                entity.Property(e => e.ValidSubId).HasColumnName("valid_sub_id");
                entity.Property(e => e.Customer).HasColumnName("customer");
                entity.Property(e => e.HoursLeft).HasColumnName("hours_left");
                entity.Property(e => e.SubType).HasColumnName("sub_type");

                entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.ValidSubscriptions)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("valid_subscriptions_customer_fkey");

                entity.HasOne(d => d.SubTypeNavigation).WithMany(p => p.ValidSubscriptions)
                    .HasForeignKey(d => d.SubType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("valid_subscriptions_sub_type_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}