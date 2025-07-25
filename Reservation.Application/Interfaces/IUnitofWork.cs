namespace Reservation.Application.Interfaces
{
    public interface IUnitofWork:IAsyncDisposable
    {
        ICompanyRepository Companies { get; }
        ICustomerRepository Customers { get; }
        IReservationRepository Reservations { get; }
        IServiceRepository Services { get; }
        ISpecialityRepository Specialities { get; }
        IStaffMemberRepository StaffMembers{ get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
