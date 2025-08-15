using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class CompanySpecialty:BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
