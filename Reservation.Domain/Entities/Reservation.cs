using Reservation.Domain.Base;
using Reservation.Domain.Enums;

namespace Reservation.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public string Title { get; set; } = null!;

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public int ReservationNumer { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        /// <summary>
        /// Reservasiya vaxtı - istifadəçinin seçdiyi vaxt (CreatedAt deyil)
        /// </summary>
        public DateTime ReservationTime { get; set; }

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
        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;


        /// <summary>
        /// Müştəri
        /// </summary>
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        public ICollection<ReservationSpecService> ReservationSpecServices { get; set; }

       
        public Reservation()
        {
            ReservationSpecServices = new List<ReservationSpecService>();
        }

    }
}
