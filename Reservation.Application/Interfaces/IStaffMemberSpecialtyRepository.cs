namespace Reservation.Application.Interfaces
{
    public interface IStaffMemberSpecialtyRepository : IGenericRepository<Domain.Entities.StaffMemberSpecialty>
    {
        Task<bool> DeleteSpecialityAsync(Guid userId, List<Guid> specialtyİds);
        Task<bool> DeleteSpecialityAsync(Guid userId, Guid specialtyİd);

        Task<bool> AddSpecialityToUserAsync(Guid userId, List<Guid> specialityİds);
        Task<bool> AddSpecialityToUserAsync(Guid userId, Guid specialityİd);
    }
}
