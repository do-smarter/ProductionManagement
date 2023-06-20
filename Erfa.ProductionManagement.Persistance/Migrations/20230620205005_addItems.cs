using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9fb3a2bc-bbad-474e-9dc5-dda55138c7c8"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("cb0d9296-f7aa-4683-a99b-183db4c75ab8"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductNumber", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { new Guid("751a413d-a2b6-4857-9fb6-2fd24e636668"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4677), "Very nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4679), "XYZ123", 100.0, 100.0 },
                    { new Guid("8da62b85-5299-409d-815d-f519256b9250"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4700), "Not so nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4702), "ABC987", 50.0, 50.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("751a413d-a2b6-4857-9fb6-2fd24e636668"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8da62b85-5299-409d-815d-f519256b9250"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductNumber", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { new Guid("9fb3a2bc-bbad-474e-9dc5-dda55138c7c8"), "Shelv", "Magdalena", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very nice piece of metal", "Magdalena", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "XYZ123", 100.0, 100.0 },
                    { new Guid("cb0d9296-f7aa-4683-a99b-183db4c75ab8"), "Shelv", "Magdalena", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Not so nice piece of metal", "Magdalena", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ABC987", 50.0, 50.0 }
                });
        }
    }
}
