using MediatR;
using Reservation.Application.Features.Specialty.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Handlers
{
    public class UpdateStatusSpecialtyCommandHandler : IRequestHandler<UpdateStatusSpecialtyCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public UpdateStatusSpecialtyCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateStatusSpecialtyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var specialty = await _unitOfWork.Specialities.GetByIdAsync(request.specialtyId);
                if(specialty == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət kateqoriyası tapılmadı" }, 200);

                var companySpec = await _unitOfWork.CompanySpecialities.FindAsync(x=>x.CompanyId == request.companyId && x.SpecialtyId == request.specialtyId);
                var model = companySpec.FirstOrDefault();

                model.IsDeleted = !model.IsDeleted;
                model.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.CompanySpecialities.Update(model);

                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət kateqoriya yenilənmədi"},200);
            }
        }
    }
}
