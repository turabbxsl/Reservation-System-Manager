namespace Reservation.Application.Features.Services.Dtos
{
    public record UpdateInSpecialtyServiceDto(Guid serviceId, string serviceName, decimal price, int duration);
}
