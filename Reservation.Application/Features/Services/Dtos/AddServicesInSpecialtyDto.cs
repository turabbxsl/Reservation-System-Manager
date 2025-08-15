namespace Reservation.Application.Features.Services.Dtos
{
    public class AddServicesInSpecialtyDto
    {
        public Guid CompanyId { get; set; }

        public Guid SpecialityId { get; set; }
        

        public List<ServiceDto> Services { get; set; } = new();

        public AddServicesInSpecialtyDto()
        {
            Services = new List<ServiceDto>();
        }
    }
}
