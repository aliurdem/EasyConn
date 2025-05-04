using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyConnect.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfile_AspNetUsers_UserId",
                table: "BusinessProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfile_BusinessProfileId",
                table: "BusinessProfileServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_Service_ServiceId",
                table: "BusinessProfileServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_BusinessProfile_BusinessProfileId",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfile",
                table: "BusinessProfile");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staffs");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "BusinessProfile",
                newName: "BusinessProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_BusinessProfileId",
                table: "Staffs",
                newName: "IX_Staffs_BusinessProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessProfile_UserId",
                table: "BusinessProfiles",
                newName: "IX_BusinessProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfiles",
                table: "BusinessProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfiles_AspNetUsers_UserId",
                table: "BusinessProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfiles_BusinessProfileId",
                table: "BusinessProfileServices",
                column: "BusinessProfileId",
                principalTable: "BusinessProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileServices_Services_ServiceId",
                table: "BusinessProfileServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_BusinessProfiles_BusinessProfileId",
                table: "Staffs",
                column: "BusinessProfileId",
                principalTable: "BusinessProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfiles_AspNetUsers_UserId",
                table: "BusinessProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_BusinessProfiles_BusinessProfileId",
                table: "BusinessProfileServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileServices_Services_ServiceId",
                table: "BusinessProfileServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_BusinessProfiles_BusinessProfileId",
                table: "Staffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfiles",
                table: "BusinessProfiles");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Staff");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "BusinessProfiles",
                newName: "BusinessProfile");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_BusinessProfileId",
                table: "Staff",
                newName: "IX_Staff_BusinessProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessProfiles_UserId",
                table: "BusinessProfile",
                newName: "IX_BusinessProfile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfile",
                table: "BusinessProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfile_AspNetUsers_UserId",
                table: "BusinessProfile",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_BusinessProfile_BusinessProfileId",
                table: "Staff",
                column: "BusinessProfileId",
                principalTable: "BusinessProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
