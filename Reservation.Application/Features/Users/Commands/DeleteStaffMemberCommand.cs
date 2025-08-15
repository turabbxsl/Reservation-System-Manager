using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public class DeleteStaffMemberCommand : IRequest<ResponseDto<bool>>
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}
