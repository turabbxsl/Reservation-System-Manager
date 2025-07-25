using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateStaffmemberservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMemberService_Services_ServiceId",
                table: "StaffMemberService");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMemberService_StaffMembers_StaffMemberId",
                table: "StaffMemberService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffMemberService",
                table: "StaffMemberService");

            migrationBuilder.RenameTable(
                name: "StaffMemberService",
                newName: "StaffMemberServices");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMemberService_ServiceId",
                table: "StaffMemberServices",
                newName: "IX_StaffMemberServices_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices",
                columns: new[] { "StaffMemberId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMemberServices_Services_ServiceId",
                table: "StaffMemberServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMemberServices_StaffMembers_StaffMemberId",
                table: "StaffMemberServices",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMemberServices_Services_ServiceId",
                table: "StaffMemberServices");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMemberServices_StaffMembers_StaffMemberId",
                table: "StaffMemberServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices");

            migrationBuilder.RenameTable(
                name: "StaffMemberServices",
                newName: "StaffMemberService");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMemberServices_ServiceId",
                table: "StaffMemberService",
                newName: "IX_StaffMemberService_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffMemberService",
                table: "StaffMemberService",
                columns: new[] { "StaffMemberId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMemberService_Services_ServiceId",
                table: "StaffMemberService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMemberService_StaffMembers_StaffMemberId",
                table: "StaffMemberService",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
