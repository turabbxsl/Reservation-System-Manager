using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {

        public CompanyRepository(ReservationDbContext context) : base(context) { }

        public async Task<Company?> GetByEmailAsync(string email) => await _context.Companies.FirstOrDefaultAsync(c => c.Email == email);
    }
}
