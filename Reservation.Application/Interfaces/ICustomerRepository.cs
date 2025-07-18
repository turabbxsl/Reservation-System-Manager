using Reservation.Domain.Entities;

namespace Reservation.Application.Interfaces
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
    }
}
