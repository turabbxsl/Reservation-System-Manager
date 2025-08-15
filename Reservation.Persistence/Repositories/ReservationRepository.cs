using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation.Domain.Entities.Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationDbContext context) : base(context) { }

        public async Task<int> ExistAsync(Guid companyId, Guid specId, DateTime dateTime)
        {
            var res = await _context.Reservations
                                .Where(r => r.CompanyId == companyId &&
                                     r.SpecialtyId == specId &&
                                     r.ReservationTime == dateTime &&
                                     r.Status != Domain.Enums.ReservationStatus.Approved)
                                .CountAsync();
           


            return res;
        }
    }
}
