using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class alterNameOf_ArchiveProductionGroupss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedProductionItems_ArchivedProductionGroupss_ProductionGroupHistoryId",
                table: "ArchivedProductionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchivedProductionGroupss",
                table: "ArchivedProductionGroupss");

            migrationBuilder.RenameTable(
                name: "ArchivedProductionGroupss",
                newName: "ArchivedProductionGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchivedProductionGroups",
                table: "ArchivedProductionGroups",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 30, 0, 9, 27, 219, DateTimeKind.Local).AddTicks(6808), new DateTime(2023, 6, 30, 0, 9, 27, 219, DateTimeKind.Local).AddTicks(6809) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 30, 0, 9, 27, 219, DateTimeKind.Local).AddTicks(6800), new DateTime(2023, 6, 30, 0, 9, 27, 219, DateTimeKind.Local).AddTicks(6802) });

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedProductionItems_ArchivedProductionGroups_ProductionGroupHistoryId",
                table: "ArchivedProductionItems",
                column: "ProductionGroupHistoryId",
                principalTable: "ArchivedProductionGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedProductionItems_ArchivedProductionGroups_ProductionGroupHistoryId",
                table: "ArchivedProductionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchivedProductionGroups",
                table: "ArchivedProductionGroups");

            migrationBuilder.RenameTable(
                name: "ArchivedProductionGroups",
                newName: "ArchivedProductionGroupss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchivedProductionGroupss",
                table: "ArchivedProductionGroupss",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedProductionItems_ArchivedProductionGroupss_ProductionGroupHistoryId",
                table: "ArchivedProductionItems",
                column: "ProductionGroupHistoryId",
                principalTable: "ArchivedProductionGroupss",
                principalColumn: "Id");
        }
    }
}
