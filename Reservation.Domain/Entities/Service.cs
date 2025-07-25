using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class Service : BaseEntity
    {
        /// <summary>
        /// Xidmətin adı ("Saç düzümü", "Ortopediya", "Dırnaq")
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Xidmətin qiyməti
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Endirim faizi
        /// </summary>
        public decimal? DiscountPercent { get; set; }


        /// <summary>
        /// Xidmət müddəti
        /// </summary>
        public int DurationInMinutes { get; set; }


        /// <summary>
        /// Açıklama / Qeydlər (opsional)
        /// </summary>
        public string? Description { get; set; }


        /// <summary>
        /// Hansi şirkətə aiddir
        /// </summary>
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        /// <summary>
        /// Xidmət hansı ixtisasa aiddir
        /// Specialty: Ortopediya → Service: “Diz müayinəsi” “Rentgen”
        /// Specialty: Berber → Service: “Saç düzümü”, “Saqqal”
        /// Specialty: Diş həkimi → “Diş çıxarılması”, “Ağartma”
        /// </summary>
        public Guid? SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<StaffMemberService> StaffMemberServices { get; set; }


        public Service()
        {
            Reservations = new List<Reservation>();
            StaffMemberServices = new List<StaffMemberService>();
        }
    }
}
