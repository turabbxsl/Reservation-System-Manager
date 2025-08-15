using AutoMapper;
using MediatR;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Handlers
{
    public class AddSpecialtyWithServicesCommandHandler : IRequestHandler<AddSpecialtyWithServicesCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddSpecialtyWithServicesCommandHandler(
            IUnitofWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseDto<bool>> Handle(AddSpecialtyWithServicesCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var isHaveSpec = await _unitOfWork.Specialities.FindAsync(x => x.Name.ToLower() == request.SpecialtyName.ToLower());
                var dbResultSpec = isHaveSpec.FirstOrDefault();

                var specialty = new Domain.Entities.Specialty
                {
                    Id = Guid.NewGuid(),
                    Name = request.SpecialtyName,
                    CreatedAt = DateTime.UtcNow
                };


                if (dbResultSpec == null)
                    await _unitOfWork.Specialities.AddAsync(specialty);

                var companySpecialty = new CompanySpecialty
                {
                    CompanyId = request.CompanyId,
                    SpecialtyId = dbResultSpec != null ? dbResultSpec.Id : specialty.Id
                };
                await _unitOfWork.CompanySpecialities.AddAsync(companySpecialty);

                foreach (var service in request.Services)
                {
                    var isHaveService = await _unitOfWork.Services.FindAsync(x => x.Name.ToLower() == service.Name.ToLower());
                    var dbResultServ = isHaveService.FirstOrDefault();

                    var serviceEntity = new Service
                    {
                        Id = Guid.NewGuid(),
                        Name = service.Name,
                        Price = service.Price,
                        DurationInMinutes = service.Duration,
                        SpecialtyId = dbResultSpec != null ? dbResultSpec.Id : specialty.Id,
                        CreatedAt = DateTime.UtcNow
                    };


                    if (dbResultServ == null)
                        await _unitOfWork.Services.AddAsync(serviceEntity);

                    var companyService = new CompanyService
                    {
                        CompanyId = request.CompanyId,
                        ServiceId = dbResultServ != null ? dbResultServ.Id : serviceEntity.Id
                    };
                    await _unitOfWork.CompaniesService.AddAsync(companyService);
                }

                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                    return ResponseDto<bool>.SuccessResponse(true, 200);
                else
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Xidmət kateqoriyası əlavə edilə bilmədi" }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət kateqoriyası əlavə edilə bilmədi -> {ex.InnerException?.Message}" }, 200);
            }
        }
    }
}
