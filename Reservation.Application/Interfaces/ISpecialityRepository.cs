using Reservation.Domain.Entities;
using Reservation.Domain.Enums;

namespace Reservation.Application.Interfaces
{
    public interface ISpecialityRepository : IGenericRepository<Domain.Entities.Specialty>
    {
        Task<List<Specialty>> GetByCompanyTypeId(CompanyType companyTypeId);
    }
}
