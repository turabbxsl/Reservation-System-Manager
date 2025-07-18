using Reservation.Domain.Entities;

namespace Reservation.Application.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
