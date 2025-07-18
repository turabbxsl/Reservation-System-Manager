using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;

namespace Reservation.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ReservationDbContext context) : base(context){ }

        public async Task<Customer?> GetByEmailAsync(string email) => await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber) => await _context.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}
