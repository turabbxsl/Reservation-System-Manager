using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Domain.Enums;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class SpecialityRepository : GenericRepository<Reservation.Domain.Entities.Specialty>, ISpecialityRepository
    {
        public SpecialityRepository(ReservationDbContext context) : base(context) { }

        public async Task<List<Specialty>> GetByCompanyTypeId(CompanyType companyTypeId)
        {
            return await _context.Specialties
        .Where(s => s.CompanyType == companyTypeId && !s.IsDeleted)
        .ToListAsync();
        }
    }
}
