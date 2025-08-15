using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class ReservationSpecService:BaseEntity
    {
        public Guid ReservationId { get; set; }
        public Reservation Reservation{ get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty{ get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
