using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class rename_ProductionComponent_to_ProductionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionItems_ProductionComponents_ProductionComponentId",
                table: "ProductionItems");

            migrationBuilder.DropTable(
                name: "ProductionComponents");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("751a413d-a2b6-4857-9fb6-2fd24e636668"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8da62b85-5299-409d-815d-f519256b9250"));

            migrationBuilder.RenameColumn(
                name: "ProductionComponentId",
                table: "ProductionItems",
                newName: "ProductionGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionItems_ProductionComponentId",
                table: "ProductionItems",
                newName: "IX_ProductionItems_ProductionGroupId");

            migrationBuilder.CreateTable(
                name: "ProductionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionGroups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductNumber", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { new Guid("450e645a-66cc-45d2-bfb7-9bb640f393c3"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3270), "Very nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3272), "XYZ123", 100.0, 100.0 },
                    { new Guid("dac8fc7b-75f9-409e-ac34-5a7c789a0c71"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3278), "Not so nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3280), "ABC987", 50.0, 50.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionItems_ProductionGroups_ProductionGroupId",
                table: "ProductionItems",
                column: "ProductionGroupId",
                principalTable: "ProductionGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionItems_ProductionGroups_ProductionGroupId",
                table: "ProductionItems");

            migrationBuilder.DropTable(
                name: "ProductionGroups");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("450e645a-66cc-45d2-bfb7-9bb640f393c3"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("dac8fc7b-75f9-409e-ac34-5a7c789a0c71"));

            migrationBuilder.RenameColumn(
                name: "ProductionGroupId",
                table: "ProductionItems",
                newName: "ProductionComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionItems_ProductionGroupId",
                table: "ProductionItems",
                newName: "IX_ProductionItems_ProductionComponentId");

            migrationBuilder.CreateTable(
                name: "ProductionComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionComponents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductNumber", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { new Guid("751a413d-a2b6-4857-9fb6-2fd24e636668"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4677), "Very nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4679), "XYZ123", 100.0, 100.0 },
                    { new Guid("8da62b85-5299-409d-815d-f519256b9250"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4700), "Not so nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 22, 50, 5, 468, DateTimeKind.Local).AddTicks(4702), "ABC987", 50.0, 50.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionItems_ProductionComponents_ProductionComponentId",
                table: "ProductionItems",
                column: "ProductionComponentId",
                principalTable: "ProductionComponents",
                principalColumn: "Id");
        }
    }
}
