using MediatR;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class DeleteStaffMemberHandler : IRequestHandler<DeleteStaffMemberCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<User> _userManager;


        public DeleteStaffMemberHandler(IUnitofWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<ResponseDto<bool>> Handle(DeleteStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var staffMember = (await _unitOfWork.StaffMembers.FindAsync(
                                    x => x.Id == request.UserId && x.CompanyId == request.CompanyId
                                )).FirstOrDefault();

                if (staffMember == null)
                    return ResponseDto<bool>.ErrorResponse("Əməkdaş tapılmadı", 200);

                staffMember.IsDeleted = true;
                _unitOfWork.StaffMembers.Update(staffMember);

                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user != null)
                    await _userManager.DeleteAsync(user);

                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { "Gözlənilməz xəta baş verdi -> ", ex.InnerException?.Message }, 200);
            }
        }
    }
}
