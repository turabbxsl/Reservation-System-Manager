using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices");


            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices",
                columns: new[] { "StaffMemberId", "ServiceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecID",
                table: "StaffMemberServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffMemberServices",
                table: "StaffMemberServices",
                columns: new[] { "StaffMemberId", "ServiceId", "SpecID" });

            migrationBuilder.CreateIndex(
                name: "IX_StaffMemberServices_SpecID",
                table: "StaffMemberServices",
                column: "SpecID");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMemberServices_Specialties_SpecID",
                table: "StaffMemberServices",
                column: "SpecID",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
