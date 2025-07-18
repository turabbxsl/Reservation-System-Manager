using MediatR;

namespace Reservation.Application.Features.Reservations.Dtos
{
    public class CreateReservationDto : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
    }
}
