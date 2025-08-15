using Reservation.Domain.Base;
using Reservation.Domain.Enums;

namespace Reservation.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; } = null!;
        public CompanyType CompanyType { get; set; }

        public int? RestMinute { get; set; }

        public ICollection<StaffMember> StaffMembers { get; set; }

        public ICollection<Service> Services { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<CompanySpecialty> CompanySpecialties { get; set; }

        public ICollection<StaffMemberSpecialty> StaffMemberSpecialties { get; set; }

        public ICollection<StaffMemberService> StaffMemberServices { get; set; }

        public ICollection<ReservationSpecService> ReservationSpecServices { get; set; }

        public ICollection<CompanyService> CompanyServices { get; set; }


        public Specialty()
        {
            StaffMembers = new List<StaffMember>();
            Services = new List<Service>();
            CompanySpecialties = new List<CompanySpecialty>();
            ReservationSpecServices = new List<ReservationSpecService>();
            Reservations = new List<Reservation>(); 
            StaffMemberSpecialties = new List<StaffMemberSpecialty>();
            StaffMemberServices = new List<StaffMemberService>();
            CompanyServices = new List<CompanyService>();
        }
    }
}
