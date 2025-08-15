using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Reservations.Queries;
using Reservation.Application.Features.Specialty.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Handlers
{
    public class GetSpecialitiesByCompanyIdQueryHandler : IRequestHandler<GetSpecialitiesByCompanyIdQuery, ResponseDto<List<SpecialtyVM>>>
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSpecialitiesByCompanyIdQueryHandler(IUnitofWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<SpecialtyVM>>> Handle(GetSpecialitiesByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var specialties = await _unitOfWork.CompanySpecialities
                                   .FindWithIncludeAsync(
                                       s => s.CompanyId == request.companyId && s.IsDeleted == false,
                                       include: query => query.Include(cs => cs.Specialty)
                                   );

                var result = _mapper.Map<List<SpecialtyVM>>(specialties);

                return ResponseDto<List<SpecialtyVM>>.SuccessResponse(result, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<SpecialtyVM>>.ErrorResponse(new List<string>() { $"Gözlənilməz xəta baş verdi -> {ex.InnerException?.Message}" }, 200);
            }
        }
    }
}
