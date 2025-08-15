using MediatR;
using Reservation.Application.Features.Specialty.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Handlers
{
    public class UpdateSpecialityCommandHandler : IRequestHandler<UpdateSpecialityCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public UpdateSpecialityCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseDto<bool>> Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var speciality = await _unitOfWork.Specialities.GetByIdAsync(request.specialityId);

                if (speciality == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət kateqoriyası tapılmadı" }, 200);

                speciality.Name = request.newSpecialityName;
                speciality.UpdatedAt = DateTime.Now;
                _unitOfWork.Specialities.Update(speciality);

                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət kateqoriyası yenilənə bilmədi -> {ex.InnerException?.Message}" }, 200);
            }
        }
    }
}
