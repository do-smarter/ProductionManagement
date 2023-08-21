using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class State_Changes_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMerged",
                table: "ProductionGroups");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "ArchivedItems");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "ArchivedItems",
                newName: "ArchiveState");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupState",
                table: "ProductionGroups");

            migrationBuilder.RenameColumn(
                name: "ArchiveState",
                table: "ArchivedItems",
                newName: "State");

            migrationBuilder.AddColumn<bool>(
                name: "IsMerged",
                table: "ProductionGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
