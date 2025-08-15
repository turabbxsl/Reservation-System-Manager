using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public record AddSpecialitiesStaffMemberCommand(Guid userId,List<Guid> specialtyIds):IRequest<ResponseDto<bool>>;
}
