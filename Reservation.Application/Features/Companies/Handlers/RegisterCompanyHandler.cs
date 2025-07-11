using MediatR;
using Reservation.Application.Features.Companies.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;

namespace Reservation.Application.Features.Companies.Handlers
{
    public class RegisterCompanyHandler : IRequestHandler<RegisterCompanyCommand, Guid>
    {
        private readonly IUnitofWork _unitofWork;

        public RegisterCompanyHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<Guid> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.model;

            var isExist = await _unitofWork.Companies.GetByEmailAsync(dto.Email);
            if (isExist is not null)
                throw new Exception("Bu email ilə artıq şirkət mövcuddur.");

            var newCompany = new Company
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email
            };

            await _unitofWork.Companies.AddAsync(newCompany);
            await _unitofWork.SaveChangesAsync();

            return newCompany.Id;
        }
    }
}
