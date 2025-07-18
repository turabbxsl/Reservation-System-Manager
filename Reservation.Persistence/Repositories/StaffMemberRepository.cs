using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class StaffMemberRepository : GenericRepository<Reservation.Domain.Entities.StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(ReservationDbContext context) : base(context) { }

        public async Task<int> CountByService(Guid companyId, Guid serviceId)
        {
            return await _context.StaffMembers
                .Include(sm => sm.Specialty)
                .ThenInclude(s => s.Services)
                .Where(sm => sm.CompanyId == companyId &&
                 sm.Specialty.Services.Any(s => s.Id == serviceId))
                .CountAsync();
        }
    }
}
