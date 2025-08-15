using Reservation.Domain.Enums;

namespace Reservation.Infrastructure.Extensions
{
    public static class HelperExtensions
    {

        public static string StatusName(this ReservationStatus status)
        {

            string statusStr = status.ToString();

            switch (status)
            {
                case ReservationStatus.Pending:
                    statusStr = "Gözlənilir";
                    break;
                case ReservationStatus.Approved:
                    statusStr = "Təsdiq edildi";
                    break;
                case ReservationStatus.Cancelled:
                    statusStr = "İmtina edilib";
                    break;
                case ReservationStatus.Completed:
                    statusStr = "Tamamlandı";
                    break;
                case ReservationStatus.Refunded:
                    statusStr = "Geri qaytarıldı";
                    break;
                case ReservationStatus.FailedDueToOverbooking:
                    statusStr = "Çox rezervasiyaya görə alınmadı";
                    break;
                case ReservationStatus.CustomerCancelled:
                    statusStr = "Müştəri ləğvi";
                    break;
                default:
                    statusStr = "Bilinmir";
                    break;
            }

            return statusStr;
        }

        public static string GenerateUniqueRandomNumbers()
        {
            Random generator = new Random();
            String r = generator.Next(0, 100000).ToString("D6");
            r += generator.Next(0, 100000).ToString("D7");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateUniqueRandomNumbers();
            }
            return r;
        }

    }
}
