using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Services;
using Reservation.Domain.Entities;

namespace Reservation.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitofWork _unitofWork;

        public CompanyService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _unitofWork.Companies.GetAllAsync();
        }

        public async Task<Company?> GetByEmailAsync(string email)
        {
            return await _unitofWork.Companies.GetByEmailAsync(email);
        }
    }
}
