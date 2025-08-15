using Reservation.Domain.Entities;

namespace Reservation.Application.Interfaces
{
    public interface IStaffMemberRepository : IGenericRepository<Domain.Entities.StaffMember>
    {
        Task<int> CountByService(Guid companyId, Guid serviceId);
        Task<int> CountBySpeciality(Guid companyId, Guid specialityId);

        Task<List<Service>> GetServicesByStaffMemberIdAsync(Guid staffMemberId);
    }
}
