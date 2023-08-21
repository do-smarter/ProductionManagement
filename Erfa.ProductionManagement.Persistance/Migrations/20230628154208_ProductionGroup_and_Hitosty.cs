using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ProductionGroup_and_Hitosty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupState",
                table: "ProductionGroups");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupState",
                table: "ProductionGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "ABC987",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 26, 22, 5, 53, 67, DateTimeKind.Local).AddTicks(1293), new DateTime(2023, 6, 26, 22, 5, 53, 67, DateTimeKind.Local).AddTicks(1295) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ProductNumber",
                keyValue: "XYZ123",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 26, 22, 5, 53, 67, DateTimeKind.Local).AddTicks(1286), new DateTime(2023, 6, 26, 22, 5, 53, 67, DateTimeKind.Local).AddTicks(1288) });
        }
    }
}
