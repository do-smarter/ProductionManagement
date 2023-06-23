using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Item_Add_Colum_Archived : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ArchivedItems");

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "ArchivedItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 23, 14, 26, 50, 835, DateTimeKind.Local).AddTicks(4331), new DateTime(2023, 6, 23, 14, 26, 50, 835, DateTimeKind.Local).AddTicks(4332) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 23, 14, 26, 50, 835, DateTimeKind.Local).AddTicks(4323), new DateTime(2023, 6, 23, 14, 26, 50, 835, DateTimeKind.Local).AddTicks(4325) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "ArchivedItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "ArchivedItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3091), new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3093) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3084), new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3086) });
        }
    }
}
