using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Reservation.Application.Features.Auth.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Services;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitofWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IUnitofWork unitOfWork, RoleManager<IdentityRole<Guid>> roleManager, IPasswordService passwordService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _passwordService = passwordService;
            _configuration = configuration;
        }


        public async Task<ResponseDto<LoginVM>> AuthenticateAsync(string email, string password)
        {
            string role = string.Empty;

            var users = _userManager.Users.ToList();
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var checkPass = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (checkPass.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    role = roles.FirstOrDefault() ?? "CompanyUser";

                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new IdentityRole<Guid>(role));

                    var loginVm = new LoginVM
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FullName = user.FullName,
                        Role = role,
                        Token = _passwordService.GenerateJwtToken(user.Email!, role, _configuration)
                    };

                    return ResponseDto<LoginVM>.SuccessResponse(loginVm, "Giriş uğurla tamamlandı", 200);
                }

                return ResponseDto<LoginVM>.ErrorResponse("Şifrə yanlışdır", 401);
            }

            var customer = await _unitOfWork.Customers.GetByEmailAsync(email);
            if (customer != null)
            {
                if (!await _roleManager.RoleExistsAsync("Customer"))
                    await _roleManager.CreateAsync(new IdentityRole<Guid>("Customer"));
                role = "Customer";

                var isCheckPass = _passwordService.Decrypt(customer.PasswordHash) == password;
                if (isCheckPass)
                {
                    var loginVm = new LoginVM
                    {
                        Id = customer.Id,
                        Email = customer.Email,
                        FullName = customer.FullName,
                        Role = role,
                        Token = _passwordService.GenerateJwtToken(customer.Email!, role, _configuration)
                    };

                    return ResponseDto<LoginVM>.SuccessResponse(loginVm, "Müştəri kimi giriş edildi", 200);
                }

                return ResponseDto<LoginVM>.ErrorResponse("Şifrə yanlışdır", 200);
            }

            return ResponseDto<LoginVM>.ErrorResponse("Bu email ilə istifadəçi tapılmadı", 200);
        }


    }
}
