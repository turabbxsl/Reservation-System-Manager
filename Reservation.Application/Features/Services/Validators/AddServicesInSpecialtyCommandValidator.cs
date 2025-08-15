using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Interfaces;

namespace Reservation.Application.Features.Services.Validators
{
    public class AddServicesInSpecialtyCommandValidator : AbstractValidator<AddServicesInSpecialtyCommand>
    {
        private readonly IUnitofWork _unitofWork;

        public AddServicesInSpecialtyCommandValidator(IUnitofWork unitofWork)
        {

            _unitofWork = unitofWork;

            RuleFor(x => x.CompanyId)
                   .NotEmpty().WithMessage("Şirkət ID boş ola bilməz");

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
                    .GreaterThan(5).WithMessage("Xidmət vaxtı minimum 5 dəqiqə ola bilər");
                });

            RuleFor(x => x)
                .MustAsync(BeUniqueServiceName)
                .WithMessage("Bu xidmət adı mövcuddur");

        }

        private async Task<bool> BeUniqueServiceName(AddServicesInSpecialtyCommand dto, CancellationToken cancellationToken)
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
}
