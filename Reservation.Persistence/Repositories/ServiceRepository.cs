using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ReservationDbContext context) : base(context) { }

        public async Task<List<Service>> GetBySpecialtyId(Guid specialtyId)
        {
            return await _context.Services
        .Where(s => s.SpecialtyId == specialtyId && !s.IsDeleted)
        .ToListAsync();
        }

        public Task<List<Service>> GetServicesByCompanyId(Guid companyId)
        {
            return _context.CompaniesServices
                .Where(cs => cs.CompanyId == companyId && cs.Service.IsDeleted == false)
                .Select(cs => cs.Service)
                .ToListAsync();
        }
    }
}
