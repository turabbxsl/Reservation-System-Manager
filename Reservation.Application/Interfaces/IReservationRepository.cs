namespace Reservation.Application.Interfaces
{
    public interface IReservationRepository:IGenericRepository<Reservation.Domain.Entities.Reservation>
    {
        Task<int> ExistAsync(Guid companyId, Guid serviceId, DateTime dateTime);
    }
}
