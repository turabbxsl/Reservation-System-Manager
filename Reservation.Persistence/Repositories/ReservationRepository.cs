using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation.Domain.Entities.Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationDbContext context) : base(context) { }

        public async Task<int> ExistAsync(Guid companyId, Guid serviceId, DateTime dateTime)
        {
            var res = await _context.Reservations
                                .Where(r => r.CompanyId == companyId &&
                                     r.ServiceId == serviceId &&
                                     r.ReservationTime == dateTime)
                                .CountAsync();
           


            return res;
        }
    }
}
