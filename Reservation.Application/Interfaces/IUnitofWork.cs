namespace Reservation.Application.Interfaces
{
    public interface IUnitofWork:IAsyncDisposable
    {
        ICompanyRepository Companies { get; }
        ICompanyServiceRepository CompaniesService { get; }
        ICompanySpecialtyRepository CompanySpecialities { get; }

        ICustomerRepository Customers { get; }
        IUserRepository Users { get; }

        IServiceRepository Services { get; }
        ISpecialityRepository Specialities { get; }

        IStaffMemberRepository StaffMembers{ get; }
        IStaffMemberSpecialtyRepository StaffMembersSpecialty { get; }
        IStaffMembersServicesRepository StaffMembersServices{ get; }

        IReservationSpecServiceRepository ReservationSpecService{ get; }
        IReservationRepository Reservations { get; }

        Task<int> SaveChangesAsync();
    }
}
