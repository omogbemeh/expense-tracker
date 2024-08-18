﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionCategories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_Name",
                table: "TransactionCategories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCategories_Name",
                table: "TransactionCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
