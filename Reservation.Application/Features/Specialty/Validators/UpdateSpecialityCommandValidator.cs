using FluentValidation;
using Reservation.Application.Features.Specialty.Commands;

namespace Reservation.Application.Features.Specialty.Validators
{
    public class UpdateSpecialityCommandValidator : AbstractValidator<UpdateSpecialityCommand>
    {
        public UpdateSpecialityCommandValidator()
        {
            RuleFor(x => x.newSpecialityName)
                   .NotEmpty().WithMessage("Xidmət kateqoriyasının adı boş ola bilməz")
                   .MinimumLength(3).WithMessage("Xidmət kateqoriyasının adı minimum 3 simvol olmalıdır")
                   .MaximumLength(500).WithMessage("Xidmət kateqoriyasının adı maximum 500 simvol olmalıdır");
        }

    }
}
