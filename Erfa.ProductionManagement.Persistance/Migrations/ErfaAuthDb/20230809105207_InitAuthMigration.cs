using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.ProductionManagement.Persistance.Migrations.ErfaAuthDb
{
    /// <inheritdoc />
    public partial class InitAuthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationIdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationIdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationIdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4864bdfb-336b-4b66-98dc-fa6ef10800a1", "2", "Worker", "Worker" },
                    { "54b3795e-728f-4925-a13a-5147e9fe6fcd", "2", "User", "User" },
                    { "b8cd9a8d-e127-45e9-8f90-07ae1bc7d1d5", "1", "ProdAdmin", "ProdAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationIdentityRole");
        }
    }
}
