using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public record DeleteServiceByStaffMemberCommand(Guid UserId,Guid ServiceId):IRequest<ResponseDto<bool>>;
}
