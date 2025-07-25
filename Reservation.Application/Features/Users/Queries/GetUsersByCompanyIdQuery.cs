using MediatR;
using Reservation.Application.Features.Users.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Queries
{
    public class GetUsersByCompanyIdQuery : IRequest<ResponseDto<List<GetUsersByCompanyVM>>>
    {
        public Guid CompanyId { get; set; }

        public GetUsersByCompanyIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
