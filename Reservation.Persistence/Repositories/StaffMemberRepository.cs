using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
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

        public async Task<List<Service>> GetServicesByStaffMemberIdAsync(Guid staffMemberId)
        {
            return await _context.StaffMemberServices
            .Where(sms => sms.StaffMemberId == staffMemberId)
            .Include(sms => sms.Service)
            .Select(sms => sms.Service)
            .ToListAsync();
        }
    }
}
