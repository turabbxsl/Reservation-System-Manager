using MediatR;
using Reservation.Application.Features.Customers.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Commands
{
    public record UpdateCustomerCommand(Guid customerId,UpdateCustomerCommandDto dto):IRequest<ResponseDto<bool>>;
}
