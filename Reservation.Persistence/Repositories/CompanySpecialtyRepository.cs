using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class CompanySpecialtyRepository : GenericRepository<Reservation.Domain.Entities.CompanySpecialty>, ICompanySpecialtyRepository
    {
        public CompanySpecialtyRepository(ReservationDbContext context) : base(context) { }
    }
}
