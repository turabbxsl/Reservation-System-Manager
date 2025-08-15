using Reservation.Domain.Base;
using Reservation.Domain.Enums;

namespace Reservation.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Website { get; set; }
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Xidmətin başlanğıc saatı
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// Xidmətin bitiş saatı
        /// </summary>
        public string EndTime { get; set; }

        public CompanyType Type { get; set; } // Enum: Hospital, Dentist, Barber, BeautySalon

        /// <summary>
        /// Sistemdə giriş edən istifadəçilər
        /// </summary>
        public ICollection<User> Users { get; set; }

        ///// <summary>
        ///// Şirkətə aid xidmətlər
        ///// </summary>
        //public ICollection<Service> Services { get; set; }

        /// <summary>
        /// İşçi heyəti (həkimlər, ustalar və s.)
        /// </summary>
        public ICollection<StaffMember> StaffMembers { get; set; }


        /// <summary>
        /// Reytinlər
        /// </summary>
        public ICollection<CompanyReview> CompanyReviews { get; set; }


        /// <summary>
        /// Rezervasiyalari
        /// </summary>
        public ICollection<Reservation> Reservations { get; set; }


        /// <summary>
        /// Saheleri
        /// </summary>
        /*public ICollection<Specialty> Specialties { get; set; }*/

        public ICollection<CompanySpecialty> CompanySpecialties { get; set; }

        public ICollection<CompanyService> CompanyServices { get; set; }


        public Company()
        {
            Users = new List<User>();
            //Services = new List<Service>();
            StaffMembers = new List<StaffMember>();
            CompanyReviews = new List<CompanyReview>();
            Reservations = new List<Reservation>();
            /*            Specialties = new List<Specialty>();*/
            CompanySpecialties = new List<CompanySpecialty>();
            CompanyServices = new List<CompanyService>();
        }

    }
}
