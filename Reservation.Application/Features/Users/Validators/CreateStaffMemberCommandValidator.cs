using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Users.Commands;
using Reservation.Domain.Entities;

namespace Reservation.Application.Features.Users.Validators
{
    public class CreateStaffMemberCommandValidator : AbstractValidator<CreateStaffMemberCommand>
    {
        private readonly UserManager<User> _userManager;


        public CreateStaffMemberCommandValidator(UserManager<User> userManager)
        {

            _userManager = userManager;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Əməkdaş adı boş ola bilməz")
                .MinimumLength(3).WithMessage("Əməkdaş adı minimum 3 simvol olmalıdır")
                .MaximumLength(50).WithMessage("Əməkdaş adı maksimum 50 simvol ola bilər");

            RuleFor(x => x.Username)
                 .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz")
                .MinimumLength(4).WithMessage("İstifadəçi adı minimum 4 simvol olmalıdır")
                .MaximumLength(20).WithMessage("İstifadəçi adı maksimum 20 simvol ola bilər")
                .MustAsync(BeUniqueUsername).WithMessage("Bu istifadəçi adı artıq mövcuddur");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email boş ola bilməz")
               .Matches(@"^[^\s@]+@[^\s@]+\.[a-zA-Z]{2,}$")
               .WithMessage("Email formatı düzgün deyil")
               .MustAsync(BeUniqueEmail).WithMessage("Bu email artıq mövcuddur");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                .Matches(@"^\(\d{2}\)-\d{3}-\d{2}-\d{2}$")
                .WithMessage("Telefon formatı düzgün deyil. Məsələn: (70)-123-45-67");

            RuleFor(x => x.CompanyId)
               .NotEmpty().WithMessage("Şirkət ID boş ola bilməz");

            RuleFor(x => x.SpecialtyId)
                .NotEmpty().WithMessage("İxtisas seçilməlidir");

            RuleFor(x => x.ServiceIds)
                .NotNull().WithMessage("Xidmətlər seçilməlidir")
                .Must(s => s.Any()).WithMessage("Ən azı bir xidmət seçilməlidir");

        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null;
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }
    }
}
