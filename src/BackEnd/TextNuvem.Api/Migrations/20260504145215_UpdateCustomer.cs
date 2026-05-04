using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextNuvem.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ChangesDate",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LastProjectIdUpdate",
                table: "Customer",
                column: "LastProjectIdUpdate",
                unique: true,
                filter: "[LastProjectIdUpdate] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_LastProjectUpdateId",
                table: "Customer",
                column: "LastProjectIdUpdate",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_LastProjectUpdateId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LastProjectIdUpdate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ChangesDate",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "Projects",
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
    }
}
