using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Users.Dtos;
using Reservation.Domain.Entities;

namespace Reservation.Application.Features.Users.Validators
{
    public class UpdateStaffMemberDtoValidator : AbstractValidator<UpdateStaffMemberDto>
    {
        private readonly UserManager<User> _userManager;

        public UpdateStaffMemberDtoValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName boş ola bilməz")
            .MaximumLength(100).WithMessage("FullName 100 simvoldan uzun ola bilməz");

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

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                .Matches(@"^\(\d{2}\)-\d{3}-\d{2}-\d{2}$")
                .WithMessage("Telefon formatı düzgün deyil. Məsələn: (70)-123-45-67");
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
