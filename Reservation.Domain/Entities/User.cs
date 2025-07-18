using Microsoft.AspNetCore.Identity;

namespace Reservation.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;
        //public string Email { get; set; } = null!;
        //public string PasswordHash { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
