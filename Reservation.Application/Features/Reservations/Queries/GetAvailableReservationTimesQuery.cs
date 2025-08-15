using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Queries
{
    public class GetAvailableReservationTimesQuery:IRequest<ResponseDto<Dictionary<string, bool>>>
    {
        public Guid CompanyId { get; set; }
        public Guid SpecialtyId { get; set; }
        public DateTime Date { get; set; }
    }
}
