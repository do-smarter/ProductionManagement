using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ProductionGroup_and_Hitosty_V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MergedProductionGroupId",
                table: "ArchivedProductionItems");

            migrationBuilder.DropColumn(
                name: "UnitedProductionGroupId",
                table: "ArchivedProductionItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionGroupHistoryId",
                table: "ArchivedProductionItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArchivedProductionGroupss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArchivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArchiveState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedProductionGroupss", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 23, 53, 33, 817, DateTimeKind.Local).AddTicks(7374), new DateTime(2023, 6, 28, 23, 53, 33, 817, DateTimeKind.Local).AddTicks(7376) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 23, 53, 33, 817, DateTimeKind.Local).AddTicks(7366), new DateTime(2023, 6, 28, 23, 53, 33, 817, DateTimeKind.Local).AddTicks(7368) });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedProductionItems_ProductionGroupHistoryId",
                table: "ArchivedProductionItems",
                column: "ProductionGroupHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedProductionItems_ArchivedProductionGroupss_ProductionGroupHistoryId",
                table: "ArchivedProductionItems",
                column: "ProductionGroupHistoryId",
                principalTable: "ArchivedProductionGroupss",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedProductionItems_ArchivedProductionGroupss_ProductionGroupHistoryId",
                table: "ArchivedProductionItems");

            migrationBuilder.DropTable(
                name: "ArchivedProductionGroupss");

            migrationBuilder.DropIndex(
                name: "IX_ArchivedProductionItems_ProductionGroupHistoryId",
                table: "ArchivedProductionItems");

            migrationBuilder.DropColumn(
                name: "ProductionGroupHistoryId",
                table: "ArchivedProductionItems");

            migrationBuilder.AddColumn<Guid>(
                name: "MergedProductionGroupId",
                table: "ArchivedProductionItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UnitedProductionGroupId",
                table: "ArchivedProductionItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 17, 49, 42, 320, DateTimeKind.Local).AddTicks(8776), new DateTime(2023, 6, 28, 17, 49, 42, 320, DateTimeKind.Local).AddTicks(8778) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 17, 49, 42, 320, DateTimeKind.Local).AddTicks(8769), new DateTime(2023, 6, 28, 17, 49, 42, 320, DateTimeKind.Local).AddTicks(8771) });
        }
    }
}
