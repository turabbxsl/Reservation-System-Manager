using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class SpecialityRepository : GenericRepository<Reservation.Domain.Entities.Specialty>, ISpecialityRepository
    {
        public SpecialityRepository(ReservationDbContext context) : base(context) { }

    }
}
