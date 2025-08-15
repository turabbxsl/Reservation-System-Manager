namespace Reservation.Application.Features.Customers.Dtos
{
    public record UpdatePasswordCommandDto(string currentPassword,string newPassword);
}
