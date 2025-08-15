using MediatR;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class DeleteSpecialtyByStaffMemberCommandHandler : IRequestHandler<DeleteSpecialtyByStaffMemberCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitofWork;

        public DeleteSpecialtyByStaffMemberCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteSpecialtyByStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var staffMember = await _unitofWork.StaffMembers.GetByIdAsync(request.UserId);
                if (staffMember == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Əməkdaş tapılmadı" }, 200);

                var specialty = await _unitofWork.Specialities.GetByIdAsync(request.SpecialtyId);
                if (specialty == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət kateqoriyası tapılmadı" }, 200);

                var staffMemberSpecialty = await _unitofWork.StaffMembersSpecialty.FindAsync(x => x.SpecialtyId == request.SpecialtyId && x.StaffMemberId == request.UserId);
                if (staffMemberSpecialty == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xidmət kateqoriyası tapılmadı" }, 200);

                var staffMemberServices = await _unitofWork.StaffMembersServices.FindAsync(x => x.SpecialtyID == request.SpecialtyId && x.StaffMemberId == request.UserId);
                if (staffMemberServices.Any())
                await _unitofWork.StaffMembersServices.DeleteServicesAsync(staffMember.Id, specialty.Id, staffMemberServices.Select(x=>x.ServiceId).ToList());

                await _unitofWork.StaffMembersSpecialty.DeleteSpecialityAsync(staffMember.Id, specialty.Id);
                await _unitofWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Xİdmət kateqoriyası silinə bilmədi" }, 200);
            }
        }
    }
}
