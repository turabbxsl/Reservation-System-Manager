using MediatR;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class CreateStaffMemberHandler : IRequestHandler<CreateStaffMemberCommand, ResponseDto<Guid>>
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public CreateStaffMemberHandler(IUnitofWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseDto<Guid>> Handle(CreateStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = request.FullName,
                    UserName = request.Username,
                    Email = request.Email,
                    PhoneNumber = request.Phone,
                    CompanyId = request.CompanyId
                };

                var userResult = await _userManager.CreateAsync(newUser, "DefaultPassword123!");

                if (!userResult.Succeeded)
                    return ResponseDto<Guid>.ErrorResponse(string.Join(", ", userResult.Errors.Select(e => e.Description)), 200);

                var staffMember = new StaffMember
                {
                    Id = newUser.Id,
                    FullName = request.FullName,
                    CompanyId = request.CompanyId,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                var specialty = await _unitOfWork.Specialities.GetByIdAsync(request.SpecialtyId);
                if (specialty == null)
                    return ResponseDto<Guid>.ErrorResponse(new List<string>() { $"Xidmət kateqoriyası tapılmadı" }, 200);

                await _unitOfWork.StaffMembersSpecialty.AddAsync(new StaffMemberSpecialty()
                {
                    Id = Guid.NewGuid(),
                    SpecialtyId = specialty.Id,
                    StaffMemberId = staffMember.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                });

                if (request.ServiceIds != null && request.ServiceIds.Any())
                {
                    foreach (var serviceId in request.ServiceIds)
                    {
                        await _unitOfWork.StaffMembersServices.AddAsync(new StaffMemberService
                        {
                            Id = Guid.NewGuid(),
                            StaffMemberId = staffMember.Id,
                            SpecialtyID = specialty.Id,
                            ServiceId = serviceId,
                            CreatedAt = DateTime.UtcNow,
                            IsDeleted = false
                        });
                    }
                }

                await _unitOfWork.StaffMembers.AddAsync(staffMember);
                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<Guid>.SuccessResponse(staffMember.Id, 200);
            }
            catch (Exception)
            {
                return ResponseDto<Guid>.ErrorResponse(new List<string>() { $"Gözlənilməz xəta baş verdi" }, 200);
            }
        }
    }
}
