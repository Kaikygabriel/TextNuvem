using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextNuvem.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastProjectIdUpdate",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId1",
                table: "Projects",
                column: "CustomerId1",
                unique: true,
                filter: "[CustomerId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customer_CustomerId1",
                table: "Projects",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customer_CustomerId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CustomerId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LastProjectIdUpdate",
                table: "Customer");
        }
    }
}
