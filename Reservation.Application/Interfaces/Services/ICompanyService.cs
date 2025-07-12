using Reservation.Domain.Entities;

namespace Reservation.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetByEmailAsync(string email);
    }
}
