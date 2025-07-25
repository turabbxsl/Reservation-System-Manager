using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class StaffMemberService:BaseEntity
    {
        public Guid StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
