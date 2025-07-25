namespace Reservation.Application.Features.Auth.ViewModels
{
    public class LoginVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public string ServiceName { get; set; }
        public string SpecialityName { get; set; }

        public string? CompanyName { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
