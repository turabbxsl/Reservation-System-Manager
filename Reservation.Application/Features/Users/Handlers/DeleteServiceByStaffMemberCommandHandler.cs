using MediatR;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class DeleteServiceByStaffMemberCommandHandler : IRequestHandler<DeleteServiceByStaffMemberCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitofWork;

        public DeleteServiceByStaffMemberCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteServiceByStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var staffMember = await _unitofWork.StaffMembers.GetByIdAsync(request.UserId);
                if (staffMember == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Əməkdaş tapılmadı" }, 200);

                var service = await _unitofWork.Services.GetByIdAsync(request.ServiceId);
                if (service == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət tapılmadı" }, 200);

                var staffMemberService = await _unitofWork.StaffMembersServices.FindAsync(x=>x.ServiceId == request.ServiceId && x.StaffMemberId == request.UserId);
                if (staffMemberService == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət tapılmadı" }, 200);

                await _unitofWork.StaffMembersServices.DeleteServiceAsync(staffMember.Id, service.Id);
                await _unitofWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xİdmət silinə bilmədi" }, 200);
            }
        }
    }
}
