namespace Reservation.Domain.Exceptions.Customer
{
    public class PhoneNumberAlreadyExistsException:Exception
    {
        public PhoneNumberAlreadyExistsException(string phoneNumber)
        : base($"Bu telefon nömrəsi ilə müştəri mövcuddur: {phoneNumber}") { }
    }
}
