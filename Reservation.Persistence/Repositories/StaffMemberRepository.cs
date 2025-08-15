using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Services.ViewModels;
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
      .Where(sm => sm.CompanyId == companyId &&
                   sm.StaffMemberServices.Any(sms => sms.ServiceId == serviceId))
      .CountAsync();
        }

        public async Task<int> CountBySpeciality(Guid companyId, Guid specialityId)
        {
            var staffMembersIds = await _context.StaffMembers.Include(x => x.StaffMemberSpecialties).Where(sm => sm.CompanyId == companyId).Select(x=>x.Id).ToListAsync();
            var specialitiesCount =  await _context.StaffMemberSpecialty.Where(sms => sms.SpecialtyId == specialityId && staffMembersIds.Contains(sms.StaffMemberId)).CountAsync();
            return specialitiesCount;
        }

        public async Task<List<Service>> GetServicesByStaffMemberIdAsync(Guid staffMemberId)
        {
            return await _context.StaffMemberServices
            .Where(sms => sms.StaffMemberId == staffMemberId)
            .Include(sms => sms.Service)
            .Where(x => x.IsDeleted == false)
            .Select(sms => sms.Service)
            .ToListAsync();
        }
    }
}
