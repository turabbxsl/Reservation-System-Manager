using MediatR;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class UpdateInSpecialtyServiceCommandHandler : IRequestHandler<UpdateInSpecialtyServiceCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public UpdateInSpecialtyServiceCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateInSpecialtyServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await _unitOfWork.Services.GetByIdAsync(request.serviceId);

                if (service == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət tapılmadı" }, 200);

                service.Name = request.serviceName;
                service.UpdatedAt = DateTime.Now;
                service.Price =request.price;
                service.DurationInMinutes = request.duration;

                _unitOfWork.Services.Update(service);

                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət yenilənə bilmədi -> {ex.InnerException?.Message}" }, 200);
            }
        }
    }
}
