using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Queries
{
    public record GetAvailableReservationTimesQuery(Guid CompanyId, Guid SpecialtyId, DateTime Date) :IRequest<ResponseDto<Dictionary<string, bool>>>;
}
