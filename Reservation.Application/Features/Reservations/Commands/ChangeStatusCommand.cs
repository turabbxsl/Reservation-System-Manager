using MediatR;
using Reservation.Application.Features.Reservations.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Commands
{
    public record ChangeStatusCommand(ChangeStatusCommandDto model) : IRequest<ResponseDto<bool>>;
}
