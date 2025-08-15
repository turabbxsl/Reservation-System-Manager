using MediatR;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class UpdateStaffMemberCommandHandler : IRequestHandler<UpdateStaffMemberCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;


        public UpdateStaffMemberCommandHandler(IUnitofWork unitofWork, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepo = _unitofWork.StaffMembers;

                var user = await userRepo.GetByIdAsync(request.NewStaffMemberData.Id);
                if (user == null || user.CompanyId != request.CompanyId)
                    return ResponseDto<bool>.ErrorResponse("İstifadəçi tapılmadı", 200);

                user.FullName = request.NewStaffMemberData.FullName;
                user.UpdatedAt = DateTime.UtcNow;
                userRepo.Update(user);

                var identityUser = await _userManager.FindByIdAsync(request.NewStaffMemberData.Id.ToString());
                if (identityUser != null)
                {
                    identityUser.UserName = request.NewStaffMemberData.Username;
                    identityUser.Email = request.NewStaffMemberData.Email;
                    identityUser.PhoneNumber = request.NewStaffMemberData.PhoneNumber;

                    var identityResult = await _userManager.UpdateAsync(identityUser);
                    if (!identityResult.Succeeded)
                    {
                        var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                        return ResponseDto<bool>.ErrorResponse($"Identity update xətası: {errors}", 200);
                    }

                    if (!string.IsNullOrWhiteSpace(request.NewStaffMemberData.Role))
                    {
                        var roleName = request.NewStaffMemberData.Role;

                        if (!await _roleManager.RoleExistsAsync(roleName))
                        {
                            var roleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                            if (!roleResult.Succeeded)
                            {
                                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                                return ResponseDto<bool>.ErrorResponse($"Rol yaradılarkən xəta: {errors}", 200);
                            }
                        }

                        var currentRoles = await _userManager.GetRolesAsync(identityUser);
                        if (currentRoles.Any())
                        {
                            await _userManager.RemoveFromRolesAsync(identityUser, currentRoles);
                        }

                        var addRoleResult = await _userManager.AddToRoleAsync(identityUser, roleName);
                        if (!addRoleResult.Succeeded)
                        {
                            var errors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
                            return ResponseDto<bool>.ErrorResponse($"Rol təyin edilərkən xəta: {errors}", 200);
                        }

                    }
                }

                await _unitofWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Gözlənilməz xəta baş verdi -> {ex.InnerException?.Message}" }, 200);
            }

        }
    }
}
