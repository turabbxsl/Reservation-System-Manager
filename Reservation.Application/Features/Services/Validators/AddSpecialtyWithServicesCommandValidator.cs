using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;

public class AddSpecialtyWithServicesCommandValidator : AbstractValidator<AddSpecialtyWithServicesCommand>
{
    private readonly IUnitofWork _unitofWork;

    public AddSpecialtyWithServicesCommandValidator(IUnitofWork unitofWork)
    {
        _unitofWork = unitofWork;

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID boş ola bilməz");

        RuleFor(x => x.SpecialtyName)
            .NotEmpty().WithMessage("Xidmət kateqoriyası adı boş ola bilməz");

        RuleForEach(x => x.Services)
            .ChildRules(service =>
            {
                service.RuleFor(s => s.Name)
                    .NotEmpty().WithMessage("Xidmət adı boş ola bilməz")
                    .MinimumLength(3).WithMessage("Xidmət adı ən azı 3 simvol olmalıdır");

                service.RuleFor(s => s.Price)
                    .GreaterThanOrEqualTo(0).WithMessage("Qiymət 0 və ya mənfi ola bilməz");

                service.RuleFor(s => s.Duration)
                    .NotEmpty().WithMessage("Xidmət vaxtı boş ola bilməz")
                    .GreaterThan(1).WithMessage("Xidmət vaxtı minimum 1 dəqiqə ola bilər");
            });

        RuleFor(x => x)
            .MustAsync(async (dto, cancellation) =>
            {
                var specialty = await _unitofWork.Specialities
                    .FirstOrDefaultAsync(s => s.Name.ToLower() == dto.SpecialtyName.ToLower());

                if (specialty == null)
                    return true;

                var exists = await _unitofWork.CompanySpecialities
                    .AnyAsync(cs => cs.CompanyId == dto.CompanyId && cs.SpecialtyId == specialty.Id);

                return !exists;
            })
            .WithMessage("Bu şirkətdə bu xidmət kateqoriyası artıq mövcuddur")
            .OverridePropertyName("SpecialtyName");


        RuleFor(x => x)
            .MustAsync(BeUniqueServiceName)
            .WithMessage("Bu xidmət adı mövcuddur");

    }

    private async Task<bool> BeUniqueServiceName(AddSpecialtyWithServicesCommand dto, CancellationToken cancellationToken)
    {
        var services = await _unitofWork.CompaniesService
                .FindWithIncludeAsync(x => x.CompanyId == dto.CompanyId, query => query.Include(b => b.Service));
        var companyServiceList = services.ToList();

        foreach (var service in dto.Services)
        {
            var exists = companyServiceList.Any(s =>
                s.Service.Name.ToLower() == service.Name.ToLower());

            if (exists)
            {
                return false;
            }
        }

        return true;
    }
}
