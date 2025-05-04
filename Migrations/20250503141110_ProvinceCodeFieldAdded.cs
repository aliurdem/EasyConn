using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyConnect.Migrations
{
    /// <inheritdoc />
    public partial class ProvinceCodeFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceCode",
                table: "BusinessProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "BusinessProfiles");
        }
    }
}
