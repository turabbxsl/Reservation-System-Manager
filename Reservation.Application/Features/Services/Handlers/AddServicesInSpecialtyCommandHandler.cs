using AutoMapper;
using MediatR;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class AddServicesInSpecialtyCommandHandler : IRequestHandler<AddServicesInSpecialtyCommand, ResponseDto<bool>>
    {

        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddServicesInSpecialtyCommandHandler(
            IUnitofWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<bool>> Handle(AddServicesInSpecialtyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var specialty = await _unitOfWork.Specialities.GetByIdAsync(request.SpecialityId);
                if (specialty is null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət tapılmadı" }, 200);

                foreach (var service in request.Services)
                {
                    // yeni servis
                    var ser = new Service()
                    {
                        Id = Guid.NewGuid(),
                        Name = service.Name,
                        Price = service.Price,
                        SpecialtyId = specialty.Id,
                        DurationInMinutes = service.Duration,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    await _unitOfWork.Services.AddAsync(ser);

                    // elaqeli cedvele
                    var serRelated = new CompanyService()
                    {
                        CompanyId = request.CompanyId,
                        ServiceId = ser.Id,
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    await _unitOfWork.CompaniesService.AddAsync(serRelated);
                }

                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmətlər əlavə oluna bilmədi -> {ex.InnerException?.Message}" }, 200);
            }
        }
    }
}
