using Reservation.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Reservation.Domain.Entities
{
    public class Customer : BaseEntity
    {

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Note { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Customer()
        {
            Reservations = new List<Reservation>();
        }

    }
}
