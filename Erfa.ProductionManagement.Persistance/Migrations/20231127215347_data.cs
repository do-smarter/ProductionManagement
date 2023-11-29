using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductNumber = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProductionTimeSec = table.Column<double>(type: "double precision", nullable: false),
                    ProductWeight = table.Column<double>(type: "double precision", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    ArchivedBy = table.Column<string>(type: "text", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArchiveState = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedProductionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductionGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArchivedBy = table.Column<string>(type: "text", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArchiveState = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedProductionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ProductNumber = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProductionTimeSec = table.Column<double>(type: "double precision", nullable: false),
                    ProductWeight = table.Column<double>(type: "double precision", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ProductNumber);
                });

            migrationBuilder.CreateTable(
                name: "ProductionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedProductionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductionItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductNumber = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    RalGalv = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    ProductionGroupHistoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArchivedBy = table.Column<string>(type: "text", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArchiveState = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedProductionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedProductionItems_ArchivedProductionGroups_Production~",
                        column: x => x.ProductionGroupHistoryId,
                        principalTable: "ArchivedProductionGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemProductNumber = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    RalGalv = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    ProductionGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionItems_Items_ItemProductNumber",
                        column: x => x.ItemProductNumber,
                        principalTable: "Items",
                        principalColumn: "ProductNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionItems_ProductionGroups_ProductionGroupId",
                        column: x => x.ProductionGroupId,
                        principalTable: "ProductionGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ProductNumber", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { "ABC987", "Shelv", "Magdalena", new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9699), "Not so nice piece of metal", "Magdalena", new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9699), 50.0, 50.0 },
                    { "XYZ123", "Shelv", "Magdalena", new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9696), "Very nice piece of metal", "Magdalena", new DateTime(2023, 11, 27, 21, 53, 47, 824, DateTimeKind.Utc).AddTicks(9697), 100.0, 100.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedProductionItems_ProductionGroupHistoryId",
                table: "ArchivedProductionItems",
                column: "ProductionGroupHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionItems_ItemProductNumber",
                table: "ProductionItems",
                column: "ItemProductNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionItems_ProductionGroupId",
                table: "ProductionItems",
                column: "ProductionGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedItems");

            migrationBuilder.DropTable(
                name: "ArchivedProductionItems");

            migrationBuilder.DropTable(
                name: "ProductionItems");

            migrationBuilder.DropTable(
                name: "ArchivedProductionGroups");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ProductionGroups");
        }
    }
}
