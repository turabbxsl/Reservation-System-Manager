using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Commands
{
    public class AddServicesInSpecialtyCommand : IRequest<ResponseDto<bool>>
    {
        public Guid CompanyId { get; set; }
        public Guid SpecialityId { get; set; }
        public List<ServiceCommand> Services { get; set; } = new();

        public AddServicesInSpecialtyCommand()
        {
            Services = new();
        }
    }
}
