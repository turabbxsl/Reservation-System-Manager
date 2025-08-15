using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Users.Commands
{
    public class CreateStaffMemberCommand : IRequest<ResponseDto<Guid>>
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Guid SpecialtyId { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> ServiceIds { get; set; }
    }
}
