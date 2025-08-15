using MediatR;
using Reservation.Application.Features.Services.Queries;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class GetServicesBySpecialtyHandler : IRequestHandler<GetServicesBySpecialtyQuery, ResponseDto<List<ServiceVM>>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetServicesBySpecialtyHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<List<ServiceVM>>> Handle(GetServicesBySpecialtyQuery request, CancellationToken cancellationToken)
        {


            var services = await _unitofWork.Services
                .FindAsync(x => x.SpecialtyId == request.SpecialtyId);

            var result = services.Select(s => new ServiceVM
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Duration = s.DurationInMinutes
            }).ToList();

            return ResponseDto<List<ServiceVM>>.SuccessResponse(result, 200);


        }
    }
}
