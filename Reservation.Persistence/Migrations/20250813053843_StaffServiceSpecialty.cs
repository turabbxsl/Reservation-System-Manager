using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StaffServiceSpecialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecialtyID",
                table: "StaffMemberServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices",
                columns: new[] { "StaffMemberId", "ServiceId", "SpecialtyID" });

            migrationBuilder.CreateIndex(
                name: "IX_StaffMemberServices_SpecialtyID",
                table: "StaffMemberServices",
                column: "SpecialtyID");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMemberServices_Specialties_SpecialtyID",
                table: "StaffMemberServices",
                column: "SpecialtyID",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMemberServices_Specialties_SpecialtyID",
                table: "StaffMemberServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices");

            migrationBuilder.DropIndex(
                name: "IX_StaffMemberServices_SpecialtyID",
                table: "StaffMemberServices");

            migrationBuilder.DropColumn(
                name: "SpecialtyID",
                table: "StaffMemberServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices",
                columns: new[] { "StaffMemberId", "ServiceId" });
        }
    }
}
