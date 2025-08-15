namespace Reservation.Application.Features.Services.Dtos
{
    public class AddSpecialtyWithServicesDto
    {
        public Guid CompanyId { get; set; }
        public string SpecialtyName { get; set; }

        public List<ServiceDto> Services { get; set; } = new();

        public AddSpecialtyWithServicesDto()
        {
            Services = new List<ServiceDto>();
        }
    }

    public class ServiceDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
