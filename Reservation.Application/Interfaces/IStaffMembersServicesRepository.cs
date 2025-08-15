namespace Reservation.Application.Interfaces
{
    public interface IStaffMembersServicesRepository : IGenericRepository<Domain.Entities.StaffMemberService>
    {
        Task<bool> DeleteServicesAsync(Guid userId,Guid specialtyId, List<Guid> serviceId);
        Task<bool> DeleteServiceAsync(Guid userId, Guid serviceId);

        Task<bool> AddServicesToUserAsync(Guid userId, List<Guid> serviceIds);
        Task<bool> AddSpecialityServiceAsync(Guid userId, Guid specialityİd, Guid serviceId);
    }
}
