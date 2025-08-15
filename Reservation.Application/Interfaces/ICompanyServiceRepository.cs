namespace Reservation.Application.Interfaces
{
    public interface ICompanyServiceRepository : IGenericRepository<Domain.Entities.CompanyService>
    {
        Task<bool> ExistsAsync(Guid companyId, Guid serviceId);
    }
}
