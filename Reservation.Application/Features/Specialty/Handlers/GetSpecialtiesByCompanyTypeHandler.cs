using MediatR;
using Reservation.Application.Features.Specialty.Queries;
using Reservation.Application.Features.Specialty.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Handlers
{
    public class GetSpecialtiesByCompanyTypeHandler : IRequestHandler<GetSpecialtiesByCompanyTypeQuery, ResponseDto<List<SpecialtyVM>>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetSpecialtiesByCompanyTypeHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<List<SpecialtyVM>>> Handle(GetSpecialtiesByCompanyTypeQuery request, CancellationToken cancellationToken)
        {
            var specialties = await _unitofWork.Specialities
                .GetByCompanyTypeId(request.CompanyTypeId);

            var result = specialties.Select(s => new SpecialtyVM
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return ResponseDto<List<SpecialtyVM>>.SuccessResponse(result, 200);
        }
    }
}
