using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ProductionGroup_and_Hitosty_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductionGroupHistoryId",
                table: "ProductionItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArchivedGroups",
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
                    table.PrimaryKey("PK_ArchivedGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedProductionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RalGalv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    MergedProductionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitedProductionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArchivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArchiveState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedProductionItems", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 17, 47, 27, 464, DateTimeKind.Local).AddTicks(4708), new DateTime(2023, 6, 28, 17, 47, 27, 464, DateTimeKind.Local).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 17, 47, 27, 464, DateTimeKind.Local).AddTicks(4701), new DateTime(2023, 6, 28, 17, 47, 27, 464, DateTimeKind.Local).AddTicks(4703) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionItems_ProductionGroupHistoryId",
                table: "ProductionItems",
                column: "ProductionGroupHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionItems_ArchivedGroups_ProductionGroupHistoryId",
                table: "ProductionItems",
                column: "ProductionGroupHistoryId",
                principalTable: "ArchivedGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionItems_ArchivedGroups_ProductionGroupHistoryId",
                table: "ProductionItems");

            migrationBuilder.DropTable(
                name: "ArchivedGroups");

            migrationBuilder.DropTable(
                name: "ArchivedProductionItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductionItems_ProductionGroupHistoryId",
                table: "ProductionItems");

            migrationBuilder.DropColumn(
                name: "ProductionGroupHistoryId",
                table: "ProductionItems");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 17, 42, 8, 190, DateTimeKind.Local).AddTicks(4493), new DateTime(2023, 6, 28, 17, 42, 8, 190, DateTimeKind.Local).AddTicks(4494) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 28, 17, 42, 8, 190, DateTimeKind.Local).AddTicks(4485), new DateTime(2023, 6, 28, 17, 42, 8, 190, DateTimeKind.Local).AddTicks(4487) });
        }
    }
}
