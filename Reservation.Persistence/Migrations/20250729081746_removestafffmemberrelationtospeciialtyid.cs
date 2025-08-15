using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removestafffmemberrelationtospeciialtyid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySpecialty_Companies_CompanyId",
                table: "CompanySpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySpecialty_Specialties_SpecialtyId",
                table: "CompanySpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_Specialties_SpecialtyId",
                table: "StaffMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySpecialty",
                table: "CompanySpecialty");

            migrationBuilder.RenameTable(
                name: "CompanySpecialty",
                newName: "CompaniesSpeciality");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySpecialty_SpecialtyId",
                table: "CompaniesSpeciality",
                newName: "IX_CompaniesSpeciality_SpecialtyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecialtyId",
                table: "StaffMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompaniesSpeciality",
                table: "CompaniesSpeciality",
                columns: new[] { "CompanyId", "SpecialtyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesSpeciality_Companies_CompanyId",
                table: "CompaniesSpeciality",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesSpeciality_Specialties_SpecialtyId",
                table: "CompaniesSpeciality",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_Specialties_SpecialtyId",
                table: "StaffMembers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesSpeciality_Companies_CompanyId",
                table: "CompaniesSpeciality");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesSpeciality_Specialties_SpecialtyId",
                table: "CompaniesSpeciality");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_Specialties_SpecialtyId",
                table: "StaffMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompaniesSpeciality",
                table: "CompaniesSpeciality");

            migrationBuilder.RenameTable(
                name: "CompaniesSpeciality",
                newName: "CompanySpecialty");

            migrationBuilder.RenameIndex(
                name: "IX_CompaniesSpeciality_SpecialtyId",
                table: "CompanySpecialty",
                newName: "IX_CompanySpecialty_SpecialtyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecialtyId",
                table: "StaffMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySpecialty",
                table: "CompanySpecialty",
                columns: new[] { "CompanyId", "SpecialtyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySpecialty_Companies_CompanyId",
                table: "CompanySpecialty",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySpecialty_Specialties_SpecialtyId",
                table: "CompanySpecialty",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_Specialties_SpecialtyId",
                table: "StaffMembers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
