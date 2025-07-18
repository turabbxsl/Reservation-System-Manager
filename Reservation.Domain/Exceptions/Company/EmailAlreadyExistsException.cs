namespace Reservation.Domain.Exceptions.Company
{
    public class EmailAlreadyExistsException:Exception
    {
        public EmailAlreadyExistsException(string email)
       : base($"Bu email ilə artıq şirkət mövcuddur: {email}")
        {

        }
    }
}
