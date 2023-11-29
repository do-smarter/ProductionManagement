using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class delete_category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ArchivedItems");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 11, 29, 3, 15, 9, 621, DateTimeKind.Utc).AddTicks(2609), new DateTime(2023, 11, 29, 3, 15, 9, 621, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 11, 29, 3, 15, 9, 621, DateTimeKind.Utc).AddTicks(2606), new DateTime(2023, 11, 29, 3, 15, 9, 621, DateTimeKind.Utc).AddTicks(2607) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Items",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ArchivedItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "Category", "CreatedDate", "LastModifiedDate" },
                values: new object[] { "Shelv", new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3060), new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "Category", "CreatedDate", "LastModifiedDate" },
                values: new object[] { "Shelv", new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3056), new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3057) });
        }
    }
}
