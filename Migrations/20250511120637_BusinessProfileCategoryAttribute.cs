using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyConnect.Migrations
{
    /// <inheritdoc />
    public partial class BusinessProfileCategoryAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BusinessProfiles",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kuaför" },
                    { 2, "Güzellik Merkezi" },
                    { 3, "Masaj Salonu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfiles_CategoryId",
                table: "BusinessProfiles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfiles_Category_CategoryId",
                table: "BusinessProfiles",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfiles_Category_CategoryId",
                table: "BusinessProfiles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfiles_CategoryId",
                table: "BusinessProfiles");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BusinessProfiles");
        }
    }
}
