namespace Reservation.Application.Features.Users.Dtos
{
    public class DeleteStaffMemberDto
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}
