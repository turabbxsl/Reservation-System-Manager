using Reservation.Domain.Enums;

namespace Reservation.Application.Features.Companies.Dtos
{
    public class RegisterCompanyDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public CompanyType Type { get; set; }
        public string AdminFullName { get; set; }
        public string AdminPassword { get; set; }
    }
}
