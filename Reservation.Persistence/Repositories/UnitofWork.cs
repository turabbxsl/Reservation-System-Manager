using Reservation.Application.Interfaces;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ReservationDbContext _context;

        public UnitofWork(ReservationDbContext context)
        {
            _context = context;
            Companies = new CompanyRepository(_context);
            Reservations = new ReservationRepository(_context);
        }

        public ICompanyRepository Companies { get; private set; }

        public IReservationRepository Reservations { get; private set; }

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
