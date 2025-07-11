using Reservation.Domain.Base;
using Reservation.Domain.Enums;

namespace Reservation.Domain.Entities
{
    public class Reservation:BaseEntity
    {
        public string Title { get; set; } = null!;

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        /// <summary>
        /// Reservasiya vaxtı - istifadəçinin seçdiyi vaxt (CreatedAt deyil)
        /// </summary>
        public DateTime ReservationTime { get; set; } 
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;

        /// <summary>
        /// Həkim / Berber / Usta
        /// </summary>
        public Guid? StaffMemberId { get; set; }
        public StaffMember? StaffMember { get; set; }


        /// <summary>
        /// Reservasiya işçiyə təyin olunub mu
        /// </summary>
        public bool IsAssigned => StaffMemberId != null;


        /// <summary>
        /// Göstərilən xidmət
        /// </summary>
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;

    }
}
