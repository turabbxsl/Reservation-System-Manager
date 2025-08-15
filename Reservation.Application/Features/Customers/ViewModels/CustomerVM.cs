namespace Reservation.Application.Features.Customers.ViewModels
{
    public class CustomerVM
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ClientCode { get; set; }
        public string Note { get; set; }
        public bool IsSms { get; set; }
        public bool IsEmail { get; set; }
    }
}
