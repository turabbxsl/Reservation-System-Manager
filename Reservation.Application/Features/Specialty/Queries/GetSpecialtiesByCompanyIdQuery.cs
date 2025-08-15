using MediatR;
using Reservation.Application.Features.Specialty.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Queries
{
    public record GetSpecialtiesByCompanyIdQuery(Guid CompanyId)
        : IRequest<ResponseDto<List<SpecialtyVM>>>;
}
