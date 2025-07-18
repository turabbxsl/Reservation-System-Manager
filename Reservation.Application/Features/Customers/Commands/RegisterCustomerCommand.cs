using MediatR;
using Reservation.Application.Features.Customers.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Commands
{
    public record RegisterCustomerCommand(RegisterCustomerDto model):IRequest<ResponseDto<Guid>>;
}
