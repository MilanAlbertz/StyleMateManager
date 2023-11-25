using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleMateManager.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StyleMateGarments",
                columns: new[] { "Id", "Name", "Price", "SiteUrl" },
                values: new object[] { 1, "Werkt gw man", 0f, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StyleMateGarments",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
