namespace Reservation.Domain.Exceptions.Customer
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email)
        : base($"Bu email ilə müştəri mövcuddur: {email}"){ }
    }
}
