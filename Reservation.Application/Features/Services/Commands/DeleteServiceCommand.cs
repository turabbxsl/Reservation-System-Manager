using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Commands
{
    public record DeleteServiceCommand(Guid CompanyId,Guid ServiceId) :IRequest<ResponseDto<bool>>;
}