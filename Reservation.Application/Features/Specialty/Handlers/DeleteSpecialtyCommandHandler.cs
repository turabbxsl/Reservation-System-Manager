using MediatR;
using Reservation.Application.Features.Specialty.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Specialty.Handlers
{
    public class DeleteSpecialtyCommandHandler : IRequestHandler<DeleteSpecialtyCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public DeleteSpecialtyCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseDto<bool>> Handle(DeleteSpecialtyCommand request, CancellationToken cancellationToken)
        {
            var specialty = await _unitOfWork.Specialities.GetByIdAsync(request.CompanySpecialtyId);

            var companySpecialties = await _unitOfWork.CompanySpecialities
        .FindAsync(c => c.CompanyId == request.CompanyId && c.SpecialtyId == request.CompanySpecialtyId);

            if (!companySpecialties.Any() || specialty is null)
                return ResponseDto<bool>.ErrorResponse("Xidmət kateqoriyası tapılmadı", 200);

            // Əlaqəli xidmətlər
            var relatedServices = await _unitOfWork.CompaniesService
                .FindAsync(s => s.CompanyId == request.CompanyId &&
                                s.Service.SpecialtyId == request.CompanySpecialtyId);

            if (relatedServices.Any())
                _unitOfWork.CompaniesService.RemoveRange(relatedServices);

            // Bütün CompanySpecialty
            _unitOfWork.CompanySpecialities.RemoveRange(companySpecialties);

            if (specialty?.CompanyType is null)
                _unitOfWork.Specialities.Remove(specialty);

            await _unitOfWork.SaveChangesAsync();


            return ResponseDto<bool>.SuccessResponse(true, 200);
        }

    }
}
