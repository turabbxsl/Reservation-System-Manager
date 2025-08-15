using MediatR;
using Reservation.Application.Features.Users.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public class UpdateStaffMemberCommand : IRequest<ResponseDto<bool>>
    {
        public Guid CompanyId { get; set; }
        public UpdateStaffMemberDto NewStaffMemberData { get; set; }
    }
}
