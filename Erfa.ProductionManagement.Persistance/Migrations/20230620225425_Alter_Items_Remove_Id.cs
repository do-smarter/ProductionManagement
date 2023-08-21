using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Items_Remove_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionItems_Items_ItemId",
                table: "ProductionItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductionItems_ItemId",
                table: "ProductionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("450e645a-66cc-45d2-bfb7-9bb640f393c3"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("dac8fc7b-75f9-409e-ac34-5a7c789a0c71"));

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ProductionItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "ItemProductNumber",
                table: "ProductionItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ProductionItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProductNumber",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "ProductNumber");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ProductNumber", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { "ABC987", "Shelv", "Magdalena", new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3091), "Not so nice piece of metal", "Magdalena", new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3093), 50.0, 50.0 },
                    { "XYZ123", "Shelv", "Magdalena", new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3084), "Very nice piece of metal", "Magdalena", new DateTime(2023, 6, 21, 0, 54, 25, 31, DateTimeKind.Local).AddTicks(3086), 100.0, 100.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionItems_ItemProductNumber",
                table: "ProductionItems",
                column: "ItemProductNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionItems_Items_ItemProductNumber",
                table: "ProductionItems",
                column: "ItemProductNumber",
                principalTable: "Items",
                principalColumn: "ProductNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionItems_Items_ItemProductNumber",
                table: "ProductionItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductionItems_ItemProductNumber",
                table: "ProductionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123");

            migrationBuilder.DropColumn(
                name: "ItemProductNumber",
                table: "ProductionItems");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ProductionItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "ProductionItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ProductNumber",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "ProductNumber", "ProductWeight", "ProductionTimeSec" },
                values: new object[,]
                {
                    { new Guid("450e645a-66cc-45d2-bfb7-9bb640f393c3"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3270), "Very nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3272), "XYZ123", 100.0, 100.0 },
                    { new Guid("dac8fc7b-75f9-409e-ac34-5a7c789a0c71"), "Shelv", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3278), "Not so nice piece of metal", "Magdalena", new DateTime(2023, 6, 20, 23, 29, 11, 928, DateTimeKind.Local).AddTicks(3280), "ABC987", 50.0, 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionItems_ItemId",
                table: "ProductionItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionItems_Items_ItemId",
                table: "ProductionItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
