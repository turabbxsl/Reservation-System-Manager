namespace Reservation.Domain.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
