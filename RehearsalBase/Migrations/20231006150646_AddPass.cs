using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RehearsalBase.Migrations
{
    /// <inheritdoc />
    public partial class AddPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    customer_email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CustomerPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customers_pkey", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "rehearsal_categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rehearsal_categories_pkey", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "rehearsals",
                columns: table => new
                {
                    rehearsal_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rehearsal_date = table.Column<DateOnly>(type: "date", nullable: false),
                    rehearsal_start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    rehearsal_end = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    customer = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rehearsals_pkey", x => x.rehearsal_id);
                    table.ForeignKey(
                        name: "rehearsals_category_fkey",
                        column: x => x.category,
                        principalTable: "rehearsal_categories",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "rehearsals_customer_fkey",
                        column: x => x.customer,
                        principalTable: "customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "subscription_types",
                columns: table => new
                {
                    subscription_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    hours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subscription_types_pkey", x => x.subscription_id);
                    table.ForeignKey(
                        name: "subscription_types_category_fkey",
                        column: x => x.category,
                        principalTable: "rehearsal_categories",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "valid_subscriptions",
                columns: table => new
                {
                    valid_sub_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer = table.Column<int>(type: "integer", nullable: true),
                    sub_type = table.Column<int>(type: "integer", nullable: false),
                    hours_left = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("valid_subscriptions_pkey", x => x.valid_sub_id);
                    table.ForeignKey(
                        name: "valid_subscriptions_customer_fkey",
                        column: x => x.customer,
                        principalTable: "customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "valid_subscriptions_sub_type_fkey",
                        column: x => x.sub_type,
                        principalTable: "subscription_types",
                        principalColumn: "subscription_id");
                });

            migrationBuilder.CreateIndex(
                name: "customers_customer_email_key",
                table: "customers",
                column: "customer_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rehearsals_category",
                table: "rehearsals",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_rehearsals_customer",
                table: "rehearsals",
                column: "customer");

            migrationBuilder.CreateIndex(
                name: "IX_subscription_types_category",
                table: "subscription_types",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "subscription_types_subscription_id_key",
                table: "subscription_types",
                column: "subscription_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_valid_subscriptions_customer",
                table: "valid_subscriptions",
                column: "customer");

            migrationBuilder.CreateIndex(
                name: "IX_valid_subscriptions_sub_type",
                table: "valid_subscriptions",
                column: "sub_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rehearsals");

            migrationBuilder.DropTable(
                name: "valid_subscriptions");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "subscription_types");

            migrationBuilder.DropTable(
                name: "rehearsal_categories");
        }
    }
}
