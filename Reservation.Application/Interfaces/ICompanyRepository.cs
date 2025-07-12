using Reservation.Domain.Entities;

namespace Reservation.Application.Interfaces
{
    public interface ICompanyRepository:IGenericRepository<Company>
    {
        Task<Company?> GetByEmailAsync(string email);
    }
}
