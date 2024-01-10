using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleMateManager.API.Migrations
{
    /// <inheritdoc />
    public partial class version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StyleMateGarments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "StyleMateGarments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "StyleMateGarments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "StyleMateGarments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "StyleMateGarments");

            migrationBuilder.InsertData(
                table: "StyleMateGarments",
                columns: new[] { "Id", "Name", "Price", "SiteUrl" },
                values: new object[] { 1, "Werkt gw man", 0f, "" });
        }
    }
}
