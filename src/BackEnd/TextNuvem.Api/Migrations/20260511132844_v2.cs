using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using TextNuvem.Domain.BackOffice.ValueObject;

#nullable disable

namespace TextNuvem.Api.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    hash_password = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    refresh_token = table.Column<string>(type: "TEXT", nullable: true),
                    expired_refresh_token = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    LastProjectIdUpdate = table.Column<Guid>(type: "uuid", nullable: true),
                    ChangesDate = table.Column<List<ChangesDate>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_update_date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projects_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_email",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_LastProjectIdUpdate",
                table: "customers",
                column: "LastProjectIdUpdate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_CustomerId",
                table: "projects",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "fk_customer_last_project_update_id",
                table: "customers",
                column: "LastProjectIdUpdate",
                principalTable: "projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customer_last_project_update_id",
                table: "customers");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
