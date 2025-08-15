using MediatR;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public DeleteServiceCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await _unitOfWork.Services.GetByIdAsync(request.ServiceId);
                if (service == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət tapılmadı" }, 200);

                var companyServices = await _unitOfWork.CompaniesService.FindAsync(cs => cs.ServiceId == request.ServiceId && cs.CompanyId == request.CompanyId);
                _unitOfWork.CompaniesService.RemoveRange(companyServices);

                _unitOfWork.Services.Remove(service);
                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət silinə bilmədi -> {ex.InnerException?.Message}" }, 200);
            }
        }
    }
}
