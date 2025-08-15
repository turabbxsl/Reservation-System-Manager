using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Commands
{
    public record DeleteSpecialtyCommand(Guid CompanyId, Guid CompanySpecialtyId)
        : IRequest<ResponseDto<bool>>;
}
