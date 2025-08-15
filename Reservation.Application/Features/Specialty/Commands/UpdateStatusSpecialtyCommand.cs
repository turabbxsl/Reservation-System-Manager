using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Commands
{
    public record UpdateStatusSpecialtyCommand(Guid companyId,Guid specialtyId):IRequest<ResponseDto<bool>>;
}
