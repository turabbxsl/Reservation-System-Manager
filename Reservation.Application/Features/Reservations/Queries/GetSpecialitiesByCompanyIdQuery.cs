using MediatR;
using Reservation.Application.Features.Specialty.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Queries
{
    public record GetSpecialitiesByCompanyIdQuery(Guid companyId):IRequest<ResponseDto<List<SpecialtyVM>>>;
}
