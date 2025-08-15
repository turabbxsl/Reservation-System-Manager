using MediatR;
using Reservation.Application.Features.Customers.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Commands
{
    public record UpdatePasswordCommand(Guid customerId, UpdatePasswordCommandDto model) :IRequest<ResponseDto<bool>>;
}
