using MediatR;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Queries
{
    public record GetDetailsBySpecialtyQuery(Guid CompanyId, Guid SpecialtyId) : IRequest<ResponseDto<List<ServiceVM>>>;
}


