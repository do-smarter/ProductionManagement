using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ItemProductMaterialNameinstedofProductWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductWeight",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductWeight",
                table: "ArchivedItems");

            migrationBuilder.AddColumn<string>(
                name: "Worker",
                table: "ProductionItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialProductName",
                table: "Items",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialProductName",
                table: "ArchivedProductionItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialProductName",
                table: "ArchivedItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate", "MaterialProductName" },
                values: new object[] { new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3060), new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3060), "Some other material" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate", "MaterialProductName" },
                values: new object[] { new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3056), new DateTime(2023, 11, 28, 20, 49, 2, 89, DateTimeKind.Utc).AddTicks(3057), "some material" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Worker",
                table: "ProductionItems");

            migrationBuilder.DropColumn(
                name: "MaterialProductName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaterialProductName",
                table: "ArchivedProductionItems");

            migrationBuilder.DropColumn(
                name: "MaterialProductName",
                table: "ArchivedItems");

            migrationBuilder.AddColumn<double>(
                name: "ProductWeight",
                table: "Items",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProductWeight",
                table: "ArchivedItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate", "ProductWeight" },
                values: new object[] { new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9699), new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9699), 50.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate", "ProductWeight" },
                values: new object[] { new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9696), new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9697), 100.0 });
        }
    }
}
