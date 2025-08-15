using MediatR;
using Reservation.Application.Features.Specialty.ViewModels;
using Reservation.Domain.Enums;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Queries
{
    public class GetSpecialtiesByCompanyTypeQuery : IRequest<ResponseDto<List<SpecialtyVM>>>
    {
        public CompanyType CompanyTypeId { get; set; }
    }
}
