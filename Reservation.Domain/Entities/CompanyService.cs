using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class CompanyService:BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
