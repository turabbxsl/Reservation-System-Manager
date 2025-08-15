using MediatR;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Services.Commands
{
    public class AddSpecialtyWithServicesCommand:IRequest<ResponseDto<bool>>
    {
        public Guid CompanyId { get; set; }
        public string SpecialtyName { get; set; }
        public List<ServiceCommand> Services { get; set; } = new();

        public AddSpecialtyWithServicesCommand()
        {
            Services = new();
        }
    }

    public class ServiceCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }

}
