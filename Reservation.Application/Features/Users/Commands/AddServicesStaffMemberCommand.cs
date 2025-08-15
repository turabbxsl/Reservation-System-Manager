using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public class AddServicesStaffMemberCommand : IRequest<ResponseDto<bool>>
    {
        public Guid UserId { get; set; }
        public Guid SpecialtyId { get; set; }
        public List<Guid> ServiceIds { get; set; }
    }
}
