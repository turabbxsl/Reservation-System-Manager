using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Commands
{
    public record UpdateSpecialityCommand(Guid specialityId,string newSpecialityName,int restMinute) : IRequest<ResponseDto<bool>>;

}
