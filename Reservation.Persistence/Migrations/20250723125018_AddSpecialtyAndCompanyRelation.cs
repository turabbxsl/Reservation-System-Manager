using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialtyAndCompanyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyType",
                table: "Specialties");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Specialties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_CompanyId",
                table: "Specialties",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_Companies_CompanyId",
                table: "Specialties",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_Companies_CompanyId",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_CompanyId",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Specialties");

            migrationBuilder.AddColumn<int>(
                name: "CompanyType",
                table: "Specialties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
