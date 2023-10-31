﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RehearsalBase.Data;

#nullable disable

namespace RehearsalBase.Migrations
{
    [DbContext(typeof(PostgreDbContext))]
    [Migration("20231006150646_AddPass")]
    partial class AddPass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RehearsalBase.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("customer_email");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("customer_name");

                    b.Property<string>("CustomerPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerId")
                        .HasName("customers_pkey");

                    b.HasIndex(new[] { "CustomerEmail" }, "customers_customer_email_key")
                        .IsUnique();

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("RehearsalBase.Models.Rehearsal", b =>
                {
                    b.Property<int>("RehearsalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("rehearsal_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RehearsalId"));

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("category");

                    b.Property<int?>("Customer")
                        .HasColumnType("integer")
                        .HasColumnName("customer");

                    b.Property<DateOnly>("RehearsalDate")
                        .HasColumnType("date")
                        .HasColumnName("rehearsal_date");

                    b.Property<TimeOnly>("RehearsalEnd")
                        .HasColumnType("time without time zone")
                        .HasColumnName("rehearsal_end");

                    b.Property<TimeOnly>("RehearsalStart")
                        .HasColumnType("time without time zone")
                        .HasColumnName("rehearsal_start");

                    b.HasKey("RehearsalId")
                        .HasName("rehearsals_pkey");

                    b.HasIndex("Category");

                    b.HasIndex("Customer");

                    b.ToTable("rehearsals", (string)null);
                });

            modelBuilder.Entity("RehearsalBase.Models.RehearsalCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.HasKey("CategoryId")
                        .HasName("rehearsal_categories_pkey");

                    b.ToTable("rehearsal_categories", (string)null);
                });

            modelBuilder.Entity("RehearsalBase.Models.SubscriptionType", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("subscription_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubscriptionId"));

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("category");

                    b.Property<int>("Hours")
                        .HasColumnType("integer")
                        .HasColumnName("hours");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.HasKey("SubscriptionId")
                        .HasName("subscription_types_pkey");

                    b.HasIndex("Category");

                    b.HasIndex(new[] { "SubscriptionId" }, "subscription_types_subscription_id_key")
                        .IsUnique();

                    b.ToTable("subscription_types", (string)null);
                });

            modelBuilder.Entity("RehearsalBase.Models.ValidSubscription", b =>
                {
                    b.Property<int>("ValidSubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("valid_sub_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ValidSubId"));

                    b.Property<int?>("Customer")
                        .HasColumnType("integer")
                        .HasColumnName("customer");

                    b.Property<int>("HoursLeft")
                        .HasColumnType("integer")
                        .HasColumnName("hours_left");

                    b.Property<int>("SubType")
                        .HasColumnType("integer")
                        .HasColumnName("sub_type");

                    b.HasKey("ValidSubId")
                        .HasName("valid_subscriptions_pkey");

                    b.HasIndex("Customer");

                    b.HasIndex("SubType");

                    b.ToTable("valid_subscriptions", (string)null);
                });

            modelBuilder.Entity("RehearsalBase.Models.Rehearsal", b =>
                {
                    b.HasOne("RehearsalBase.Models.RehearsalCategory", "CategoryNavigation")
                        .WithMany("Rehearsals")
                        .HasForeignKey("Category")
                        .IsRequired()
                        .HasConstraintName("rehearsals_category_fkey");

                    b.HasOne("RehearsalBase.Models.Customer", "CustomerNavigation")
                        .WithMany("Rehearsals")
                        .HasForeignKey("Customer")
                        .HasConstraintName("rehearsals_customer_fkey");

                    b.Navigation("CategoryNavigation");

                    b.Navigation("CustomerNavigation");
                });

            modelBuilder.Entity("RehearsalBase.Models.SubscriptionType", b =>
                {
                    b.HasOne("RehearsalBase.Models.RehearsalCategory", "CategoryNavigation")
                        .WithMany("SubscriptionTypes")
                        .HasForeignKey("Category")
                        .IsRequired()
                        .HasConstraintName("subscription_types_category_fkey");

                    b.Navigation("CategoryNavigation");
                });

            modelBuilder.Entity("RehearsalBase.Models.ValidSubscription", b =>
                {
                    b.HasOne("RehearsalBase.Models.Customer", "CustomerNavigation")
                        .WithMany("ValidSubscriptions")
                        .HasForeignKey("Customer")
                        .HasConstraintName("valid_subscriptions_customer_fkey");

                    b.HasOne("RehearsalBase.Models.SubscriptionType", "SubTypeNavigation")
                        .WithMany("ValidSubscriptions")
                        .HasForeignKey("SubType")
                        .IsRequired()
                        .HasConstraintName("valid_subscriptions_sub_type_fkey");

                    b.Navigation("CustomerNavigation");

                    b.Navigation("SubTypeNavigation");
                });

            modelBuilder.Entity("RehearsalBase.Models.Customer", b =>
                {
                    b.Navigation("Rehearsals");

                    b.Navigation("ValidSubscriptions");
                });

            modelBuilder.Entity("RehearsalBase.Models.RehearsalCategory", b =>
                {
                    b.Navigation("Rehearsals");

                    b.Navigation("SubscriptionTypes");
                });

            modelBuilder.Entity("RehearsalBase.Models.SubscriptionType", b =>
                {
                    b.Navigation("ValidSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}