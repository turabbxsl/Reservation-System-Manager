using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Commands
{
    public record UpdateInSpecialtyServiceCommand(Guid serviceId, string serviceName, decimal price, int duration) : IRequest<ResponseDto<bool>>;
}
