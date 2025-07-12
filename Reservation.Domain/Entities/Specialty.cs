using Reservation.Domain.Base;
using Reservation.Domain.Enums;

namespace Reservation.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; } = null!;
        public CompanyType CompanyType { get; set; }

        public ICollection<StaffMember> StaffMembers { get; set; }
        public ICollection<Service> Services { get; set; }

        public Specialty()
        {
            StaffMembers = new List<StaffMember>();
            Services = new List<Service>();
        }
    }
}
