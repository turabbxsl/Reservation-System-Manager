using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class StaffMembersServicesRepository : GenericRepository<Reservation.Domain.Entities.StaffMemberService>, IStaffMembersServicesRepository
    {
        public StaffMembersServicesRepository(ReservationDbContext context) : base(context) { }

        public async Task<bool> AddServicesToUserAsync(Guid userId,List<Guid> serviceIds)
        {
            foreach (var serviceId in serviceIds)
            {
                var exists = await _context.StaffMemberServices
                    .FirstOrDefaultAsync(x => x.StaffMemberId == userId && x.ServiceId == serviceId);

                if (exists != null)
                {
                    if (exists.IsDeleted)
                    {
                        exists.IsDeleted = false;
                        _context.StaffMemberServices.Update(exists);
                    }

                    continue;
                }

                var entity = new StaffMemberService
                {
                    Id = Guid.NewGuid(),
                    StaffMemberId = userId,
                    ServiceId = serviceId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                await AddAsync(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSpecialityServiceAsync(Guid userId, Guid specialityİd,Guid serviceId)
        {

            var entity = new StaffMemberService
            {
                Id = Guid.NewGuid(),
                StaffMemberId = userId,
                SpecialtyID = specialityİd,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteServiceAsync(Guid userId, Guid serviceId)
        {
            var serviceToDelete = await _context.StaffMemberServices
               .Where(x => x.StaffMemberId == userId && x.ServiceId == serviceId).FirstOrDefaultAsync();

            if (serviceToDelete == null)
                return false;

            serviceToDelete.IsDeleted = true;
            serviceToDelete.UpdatedAt = DateTime.UtcNow;

            _context.StaffMemberServices.Remove(serviceToDelete);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteServicesAsync(Guid userId, Guid specialtyId, List<Guid> serviceId)
        {
            var servicesToDelete = await _context.StaffMemberServices
                .Where(x => x.StaffMemberId == userId && x.SpecialtyID == specialtyId && serviceId.Contains(x.ServiceId))
                .ToListAsync();

            if (!servicesToDelete.Any())
                return false;

            foreach (var service in servicesToDelete)
            {
                _context.StaffMemberServices.Remove(service);
            }
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
