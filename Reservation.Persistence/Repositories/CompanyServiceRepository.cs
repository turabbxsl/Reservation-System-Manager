using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class CompanyServiceRepository : GenericRepository<Reservation.Domain.Entities.CompanyService>, ICompanyServiceRepository
    {
        public CompanyServiceRepository(ReservationDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(Guid companyId, Guid serviceId)
        {
            return await _context.CompaniesServices
                        .AnyAsync(cs => cs.CompanyId == companyId && cs.ServiceId == serviceId);
        }
    }
}
