using Reservation.Application.Features.Services.ViewModels;

namespace Reservation.Application.Features.Users.ViewModels
{
    public class GetUsersByCompanyVM
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<UserSpecialityVM> Specialities { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Dictionary<string, string> Role { get; set; }

        public string Status { get; set; }

        public GetUsersByCompanyVM()
        {
            Specialities = new List<UserSpecialityVM>();
        }
    }

    public class UserSpecialityVM
    {
        public Guid SpecialityId { get; set; }
        public string SpecialityName { get; set; }

        public List<ServiceVM> Services { get; set; }
    }

}
