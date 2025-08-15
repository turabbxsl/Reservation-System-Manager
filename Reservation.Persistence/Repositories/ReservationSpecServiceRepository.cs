using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class ReservationSpecServiceRepository : GenericRepository<Reservation.Domain.Entities.ReservationSpecService>, IReservationSpecServiceRepository
    {
        public ReservationSpecServiceRepository(ReservationDbContext context) : base(context) { }
    }
}
