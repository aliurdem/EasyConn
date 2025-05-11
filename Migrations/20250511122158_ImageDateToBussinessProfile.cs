using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyConnect.Migrations
{
    /// <inheritdoc />
    public partial class ImageDateToBussinessProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "BusinessProfiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "BusinessProfiles");
        }
    }
}
