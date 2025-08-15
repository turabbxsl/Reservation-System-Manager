using Reservation.Domain.Entities;

namespace Reservation.Application.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<List<Service>> GetServicesByCompanyId(Guid companyId);
        Task<List<Service>> GetBySpecialtyId(Guid specialtyId);
    }
}
