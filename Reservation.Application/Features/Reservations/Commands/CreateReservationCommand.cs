using MediatR;
using Reservation.Application.Features.Reservations.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Commands
{
    public record CreateReservationCommand(CreateReservationDto model) : IRequest<ResponseDto<Guid>>;
}
