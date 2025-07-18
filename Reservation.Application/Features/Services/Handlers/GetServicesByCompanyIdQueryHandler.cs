using MediatR;
using Reservation.Application.Features.Services.Queries;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class GetServicesByCompanyIdQueryHandler : IRequestHandler<GetServicesByCompanyQuery, ResponseDto<List<ServiceVM>>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetServicesByCompanyIdQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<List<ServiceVM>>> Handle(GetServicesByCompanyQuery request, CancellationToken cancellationToken)
        {
            var services = await _unitofWork.Services.GetServicesByCompanyId(request.CompanyId);
            var result = services.Select(x => new ServiceVM()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).ToList();

            return ResponseDto<List<ServiceVM>>.SuccessResponse(result, 200);
        }
    }
}
