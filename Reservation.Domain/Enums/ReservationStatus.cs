namespace Reservation.Domain.Enums
{
    public enum ReservationStatus
    {
        Pending = 0,           // Sadəcə rezervasiya sorğusu edilib
        Approved = 1,          // Admin və ya sistem tərəfindən təsdiqlənib
        Cancelled = 2,         // İstifadəçi və ya admin tərəfindən ləğv edilib
        Completed = 3,         // Xidmət uğurla başa çatıb
        Refunded = 4,          // Ödəniş edilib, amma yer yox idi → pul geri qaytarılıb
        FailedDueToOverbooking = 5 // Uğursuzluq — artıq yer yoxdur
    }
}
