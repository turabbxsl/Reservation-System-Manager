using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class StaffMember : BaseEntity
    {
        public string FullName { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public StaffMember()
        {
            Reservations = new List<Reservation>();
        }

    }
}
