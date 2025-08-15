using MediatR;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Handlers
{
    public class AddSpecialitiesStaffMemberCommandHandler : IRequestHandler<AddSpecialitiesStaffMemberCommand, ResponseDto<bool>>
    {

        private readonly IUnitofWork _unitofWork;

        public AddSpecialitiesStaffMemberCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<bool>> Handle(AddSpecialitiesStaffMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var staffMember = await _unitofWork.StaffMembers.GetByIdAsync(request.userId);
                if (staffMember == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"İşçi tapılmadı" }, 200);

                foreach (var spec in request.specialtyIds)
                {
                    var specialty = await _unitofWork.Specialities.GetByIdAsync(spec);
                    var existSpecialty = await _unitofWork.StaffMembersSpecialty.FirstOrDefaultAsync(x => x.StaffMemberId == staffMember.Id && x.SpecialtyId == spec);

                    if (specialty == null || existSpecialty != null)
                        continue;

                    await _unitofWork.StaffMembersSpecialty.AddSpecialityToUserAsync(staffMember.Id, spec);
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
