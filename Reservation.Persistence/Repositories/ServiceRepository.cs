using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class ServiceRepository: GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ReservationDbContext context) : base(context) { }

        public Task<List<Service>> GetServicesByCompanyId(Guid companyId) => _context.Services.Where(x => x.CompanyId == companyId).ToListAsync();
    }
}
