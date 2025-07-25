using MediatR;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Companies.Commands;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Services;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Companies.Handlers
{
    public class RegisterCompanyHandler : IRequestHandler<RegisterCompanyCommand, ResponseDto<Guid>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordService _passwordService;

        public RegisterCompanyHandler(IUnitofWork unitofWork, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IPasswordService passwordService)
        {
            _unitofWork = unitofWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _passwordService = passwordService;
        }

        public async Task<ResponseDto<Guid>> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.model;

            var isExist = await _unitofWork.Companies.GetByEmailAsync(dto.Email);
            if (isExist is not null)
            {
                return ResponseDto<Guid>.ErrorResponse("Bu email ilə artıq bir şirkət mövcuddur.", 200);
            }

            var companyId = Guid.NewGuid();

            var newCompany = new Company
            {
                Id = companyId,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Website = dto.Website,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Type = dto.Type,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _unitofWork.Companies.AddAsync(newCompany);
            await _unitofWork.SaveChangesAsync();

            if (!await _roleManager.RoleExistsAsync("CompanyAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>("CompanyAdmin"));
            }

            var user = new User
            {
                FullName = dto.AdminFullName,
                UserName = dto.Email.Split('@')[0],
                Email = dto.Email,
                CompanyId = companyId,
                PhoneNumber = dto.Phone
                //PasswordHash = _passwordService.Encrypt(dto.AdminPassword)
            };

            var result = await _userManager.CreateAsync(user, dto.AdminPassword);


            //var staffMember = new StaffMember()
            //{
            //    Id = user.Id,
            //    FullName = dto.AdminFullName,
                
            //};

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ResponseDto<Guid>.ErrorResponse(errors, 200);
            }

            await _userManager.AddToRoleAsync(user, "CompanyAdmin");

            return ResponseDto<Guid>.SuccessResponse(companyId, "Şirkət uğurla qeydiyyatdan keçdi.", 201);
        }
    }
}
