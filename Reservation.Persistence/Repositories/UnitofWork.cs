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
            Customers = new CustomerRepository(_context);
            Services = new ServiceRepository(_context);
            Specialities = new SpecialityRepository(_context);
            StaffMembers = new StaffMemberRepository(_context);
            Users = new UserRepository(_context);
            StaffMembersServices = new StaffMembersServicesRepository(_context);
            CompaniesService = new CompanyServiceRepository(_context);
            CompanySpecialities = new CompanySpecialtyRepository(_context);
            ReservationSpecService = new ReservationSpecServiceRepository(_context);
            StaffMembersSpecialty = new StaffMemberSpecialtyRepository(_context);
        }

        public ICompanyRepository Companies { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IReservationRepository Reservations { get; private set; }

        public IServiceRepository Services { get; private set; }

        public ISpecialityRepository Specialities { get; private set; }

        public IStaffMemberRepository StaffMembers { get; private set; }

        public IStaffMembersServicesRepository StaffMembersServices { get; private set; }

        public IUserRepository Users { get; private set; }

        public ICompanyServiceRepository CompaniesService { get; private set; }

        public ICompanySpecialtyRepository CompanySpecialities { get; private set; }

        public IReservationSpecServiceRepository ReservationSpecService { get; private set; }

        public IStaffMemberSpecialtyRepository StaffMembersSpecialty { get; private set; }

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
