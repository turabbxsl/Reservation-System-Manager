using Reservation.Domain.Base;

namespace Reservation.Domain.Entities
{
    public class CompanyReview:BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }


        /// <summary>
        /// 1-5 arası
        /// </summary>
        public int Rating { get; set; }


        /// <summary>
        /// Opsional rəy
        /// </summary>
        public string? Comment { get; set; }



    }
}
