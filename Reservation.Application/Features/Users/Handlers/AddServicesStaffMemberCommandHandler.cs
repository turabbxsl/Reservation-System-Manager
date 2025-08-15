using MediatR;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class AddUserServicesCommandHandler : IRequestHandler<AddServicesStaffMemberCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitofWork;

        public AddUserServicesCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<bool>> Handle(AddServicesStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var staffMember = await _unitofWork.StaffMembers.GetByIdAsync(request.UserId);
                if (staffMember is null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Əməkdaş tapılmadı" }, 200);

                foreach (var serv in request.ServiceIds)
                {
                    var service = await _unitofWork.Services.GetByIdAsync(serv);
                    var existSpecialtyService = await _unitofWork.StaffMembersServices.FirstOrDefaultAsync(x => x.StaffMemberId == staffMember.Id && x.SpecialtyID == request.SpecialtyId && x.ServiceId == serv);
                    if (service == null || existSpecialtyService != null)
                        continue;

                    await _unitofWork.StaffMembersServices.AddSpecialityServiceAsync(staffMember.Id, request.SpecialtyId, serv);
                }

                await _unitofWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Gözlənilməz xəta baş verdi" }, 200);
            }
        }
    }
}
