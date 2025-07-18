using MediatR;
using Reservation.Application.Features.Companies.Query;
using Reservation.Application.Features.Companies.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Companies.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, ResponseDto<List<CompanyVM>>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetAllCompaniesQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<List<CompanyVM>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companyList = await _unitofWork.Companies.GetAllAsync();
            var result = companyList.Select(x => new CompanyVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return ResponseDto<List<CompanyVM>>.SuccessResponse(result,200);
        }
    }
}
