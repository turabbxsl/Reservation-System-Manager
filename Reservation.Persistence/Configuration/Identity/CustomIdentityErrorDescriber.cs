using Microsoft.AspNetCore.Identity;

namespace Reservation.Persistence.Configuration.Identity
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"Şifrə ən azı {length} simvoldan ibarət olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Şifrə ən azı bir xüsusi simvol (nöqtə, vergül, !, @ və s.) içərməlidir."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Şifrə ən azı bir rəqəm (0-9) içərməlidir."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Şifrə ən azı bir böyük hərf (A-Z) içərməlidir."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Şifrə ən azı bir kiçik hərf (a-z) içərməlidir."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"E-poçt ünvanı '{email}' artıq istifadə olunur."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"İstifadəçi adı '{userName}' artıq mövcuddur."
            };
        }


    }
}
