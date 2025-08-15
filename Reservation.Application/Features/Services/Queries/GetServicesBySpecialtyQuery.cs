using MediatR;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Queries
{
    public class GetServicesBySpecialtyQuery:IRequest<ResponseDto<List<ServiceVM>>>
    {
        public Guid CompanyId { get; set; }
        public Guid SpecialtyId { get; set; }
    }
}
