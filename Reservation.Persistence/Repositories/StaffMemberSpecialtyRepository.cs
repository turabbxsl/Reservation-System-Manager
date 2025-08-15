using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class StaffMemberSpecialtyRepository : GenericRepository<Reservation.Domain.Entities.StaffMemberSpecialty>, IStaffMemberSpecialtyRepository
    {
        public StaffMemberSpecialtyRepository(ReservationDbContext context) : base(context) { }

        public async Task<bool> AddSpecialityToUserAsync(Guid userId, List<Guid> specialityİds)
        {
            foreach (var specId in specialityİds)
            {
                var exists = await _context.StaffMemberSpecialty
                    .FirstOrDefaultAsync(x => x.StaffMemberId == userId && x.SpecialtyId == specId);

                if (exists != null)
                {
                    if (exists.IsDeleted)
                    {
                        exists.IsDeleted = false;
                        _context.StaffMemberSpecialty.Update(exists);
                    }

                    continue;
                }

                var entity = new StaffMemberSpecialty
                {
                    Id = Guid.NewGuid(),
                    StaffMemberId = userId,
                    SpecialtyId = specId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                await AddAsync(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSpecialityToUserAsync(Guid userId, Guid specialityİd)
        {

            var entity = new StaffMemberSpecialty
            {
                Id = Guid.NewGuid(),
                StaffMemberId = userId,
                SpecialtyId = specialityİd,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSpecialityAsync(Guid userId, List<Guid> specialtyİds)
        {
            var specsToDelete = await _context.StaffMemberSpecialty
                .Where(x => x.StaffMemberId == userId && specialtyİds.Contains(x.SpecialtyId))
                .ToListAsync();

            if (!specsToDelete.Any())
                return false;

            foreach (var spec in specsToDelete)
            {
                spec.IsDeleted = true;
                spec.UpdatedAt = DateTime.UtcNow;

            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSpecialityAsync(Guid userId, Guid specialtyİd)
        {
            var specToDelete = await _context.StaffMemberSpecialty
                 .Where(x => x.StaffMemberId == userId && x.SpecialtyId == specialtyİd)
                 .FirstOrDefaultAsync();
            var spec = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == specialtyİd);
            if (spec == null)
                return false;

            if (specToDelete == null)
                return false;

            spec.UpdatedAt = DateTime.UtcNow;

            _context.StaffMemberSpecialty.Remove(specToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
