using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyConnect.Migrations
{
    /// <inheritdoc />
    public partial class Restrict_Delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfile_BusinessProfileId",
                table: "BusinessProfileServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_Service_ServiceId",
                table: "BusinessProfileServices");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfile_BusinessProfileId",
                table: "BusinessProfileServices",
                column: "BusinessProfileId",
                principalTable: "BusinessProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileServices_Service_ServiceId",
                table: "BusinessProfileServices",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfile_BusinessProfileId",
                table: "BusinessProfileServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_Service_ServiceId",
                table: "BusinessProfileServices");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfile_BusinessProfileId",
                table: "BusinessProfileServices",
                column: "BusinessProfileId",
                principalTable: "BusinessProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileServices_Service_ServiceId",
                table: "BusinessProfileServices",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
