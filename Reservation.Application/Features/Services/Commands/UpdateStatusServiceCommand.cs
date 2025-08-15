using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Commands
{
    public record UpdateStatusServiceCommand(Guid companyId,Guid specialityId,Guid serviceId):IRequest<ResponseDto<bool>>;
}
