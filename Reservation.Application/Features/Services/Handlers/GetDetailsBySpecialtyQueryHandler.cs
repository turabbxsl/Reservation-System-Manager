using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Services.Queries;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class GetDetailsBySpecialtyQueryHandler
         : IRequestHandler<GetDetailsBySpecialtyQuery, ResponseDto<List<ServiceVM>>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetDetailsBySpecialtyQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<List<ServiceVM>>> Handle(GetDetailsBySpecialtyQuery request, CancellationToken cancellationToken)
        {

            var services = await _unitofWork.CompaniesService
    .FindWithIncludeAsync(
        x => x.CompanyId == request.CompanyId
                  && x.Service.SpecialtyId == request.SpecialtyId,
        include: query => query.Include(cs => cs.Service)
    );

            var result = services.Select(s => new ServiceVM
            {
                Id = s.Service.Id,
                Name = s.Service.Name,
                Price = s.Service.Price,
                Duration = s.Service.DurationInMinutes,
                IsActive = s.IsDeleted
            }).ToList();

            return ResponseDto<List<ServiceVM>>.SuccessResponse(result, 200);
        }
    }
}
