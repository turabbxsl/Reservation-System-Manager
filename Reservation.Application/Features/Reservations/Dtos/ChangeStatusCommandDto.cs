using Reservation.Domain.Enums;

namespace Reservation.Application.Features.Reservations.Dtos
{
    public record ChangeStatusCommandDto(Guid reservationId,ReservationStatus status);
}
