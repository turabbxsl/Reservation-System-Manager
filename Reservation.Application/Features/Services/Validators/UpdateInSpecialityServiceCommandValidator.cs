using FluentValidation;
using Reservation.Application.Features.Services.Commands;

namespace Reservation.Application.Features.Services.Validators
{
    public class UpdateInSpecialityServiceCommandValidator : AbstractValidator<UpdateInSpecialtyServiceCommand>
    {
        public UpdateInSpecialityServiceCommandValidator()
        {
            RuleFor(s => s.serviceId)
            .NotEmpty().WithMessage("Xidmət İD boş ola bilməz");

            RuleFor(x => x.serviceName)
               .NotEmpty().WithMessage("Xidmət adı boş ola bilməz")
               .MinimumLength(3).WithMessage("Xidmət adı minimum 3 simvol olmalıdır")
               .MaximumLength(500).WithMessage("Xidmət adı maximum 500 simvol olmalıdır");

            RuleFor(s => s.price)
                       .GreaterThanOrEqualTo(0).WithMessage("Qiymət 0 və ya mənfi ola bilməz");

            RuleFor(s => s.duration)
            .NotEmpty().WithMessage("Xidmət vaxtı boş ola bilməz")
            .GreaterThan(5).WithMessage("Xidmət vaxtı minimum 5 dəqiqə ola bilər");
        }


    }
}
