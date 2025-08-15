using FluentValidation;
using Reservation.Application.Features.Customers.Commands;
using Reservation.Application.Interfaces;

namespace Reservation.Application.Features.Customers.Validators
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly IUnitofWork _unitofWork;

        public UpdateCustomerValidator(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

            RuleFor(x => x.dto.FullName.Value)
                        .NotEmpty().WithMessage("Ad/Soyad qismi boş buraxıla bilməz")
                        .MinimumLength(3).WithMessage("Ad/Soyad minimum 3 simvol olmalıdır")
                        .MaximumLength(100).WithMessage("FullName 100 simvoldan uzun ola bilməz")
                        .When(x => x.dto.FullName.IsEdit);

            RuleFor(x => x.dto.Email.Value)
                        .NotEmpty().WithMessage("Email boş ola bilməz")
                        .Matches(@"^[^\s@]+@[^\s@]+\.[a-zA-Z]{2,}$")
                        .WithMessage("Email formatı düzgün deyil")
                        .MustAsync(BeUniqueEmail).WithMessage("Bu email artıq mövcuddur")
                        .When(x => x.dto.Email.IsEdit);

            RuleFor(x => x.dto.PhoneNumber.Value)
                        .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                        .Matches(@"^\(\d{2}\)-\d{3}-\d{2}-\d{2}$")
                        .WithMessage("Telefon formatı düzgün deyil. Məsələn: (70)-123-45-67")
                        .When(x => x.dto.PhoneNumber.IsEdit);

            RuleFor(x => x.dto.Note.Value)
                        .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                        .MinimumLength(3).WithMessage("Qeyd minimum 3 simvol olmalıdır")
                        .MaximumLength(500).WithMessage("Qeyd 500 simvoldan uzun ola bilməz")
                        .When(x => x.dto.Note.IsEdit);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _unitofWork.Customers.FindAsync(x => x.Email.ToLower() == email.ToLower());
            var dbResult = user.FirstOrDefault();
            bool exists = dbResult != null;
            return !exists;
        }
    }
}
