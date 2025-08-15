using Reservation.Application.Features.Companies.ViewModels;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Domain.Enums;

namespace Reservation.Application.Features.Customers.ViewModels
{
    public class CustomerReservationVM
    {
        public Guid Id{ get; set; }

        public CompanyVM Company { get; set; }
        public CustomerSpecialtyVM Specialty { get; set; }

        public List<ServiceVM> Services { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public StatusVM Status { get; set; }

        public int ReservationNumber { get; set; }

        public  decimal TotalPrice{ get; set; }

        public CustomerReservationVM()
        {
            Services = new List<ServiceVM>();
        }

    }

    public class CustomerSpecialtyVM
    {
        public Guid Id{ get; set; }
        public string SpecialtyName{ get; set; }
    }

    public class StatusVM
    {
        public ReservationStatus Id { get; set; }
        public string StatusName { get; set; }
    }

}
