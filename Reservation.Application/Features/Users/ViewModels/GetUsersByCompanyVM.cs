using Reservation.Domain.Entities;

namespace Reservation.Application.Features.Users.ViewModels
{
    public class GetUsersByCompanyVM
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Dictionary<Guid,string>> Services { get; set; }
        public Dictionary<Guid,string> Speciality { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public GetUsersByCompanyVM()
        {
            Services = new List<Dictionary<Guid, string>>();
        }
    }
}
