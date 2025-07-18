using MediatR;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Queries
{
    public class GetServicesByCompanyQuery:IRequest<ResponseDto<List<ServiceVM>>>
    {
        public Guid CompanyId { get; set; }

        public GetServicesByCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}

