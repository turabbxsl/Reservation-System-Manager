using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDomainLayerReservationSpecServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Reservations",
                newName: "SpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations",
                newName: "IX_Reservations_SpecialtyId");

            migrationBuilder.CreateTable(
                name: "ReservationSpecService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSpecService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationSpecService_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationSpecService_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationSpecService_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSpecService_ReservationId",
                table: "ReservationSpecService",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSpecService_ServiceId",
                table: "ReservationSpecService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSpecService_SpecialtyId",
                table: "ReservationSpecService",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Specialties_SpecialtyId",
                table: "Reservations",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Specialties_SpecialtyId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationSpecService");

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "Reservations",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_SpecialtyId",
                table: "Reservations",
                newName: "IX_Reservations_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
