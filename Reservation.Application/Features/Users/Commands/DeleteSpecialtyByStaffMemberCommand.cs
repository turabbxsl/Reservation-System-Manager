using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public record DeleteSpecialtyByStaffMemberCommand(Guid UserId, Guid SpecialtyId) : IRequest<ResponseDto<bool>>;
}
