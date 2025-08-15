using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class StaffMemberSpecialty:BaseEntity
    {
        public Guid StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
