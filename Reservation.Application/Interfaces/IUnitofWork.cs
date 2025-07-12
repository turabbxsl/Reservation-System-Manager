namespace Reservation.Application.Interfaces
{
    public interface IUnitofWork:IAsyncDisposable
    {
        ICompanyRepository Companies { get; }
        IReservationRepository Reservations { get; }

        Task<int> SaveChangesAsync();
    }
}
