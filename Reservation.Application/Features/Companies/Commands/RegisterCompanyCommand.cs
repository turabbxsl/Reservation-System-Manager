using MediatR;
using Reservation.Application.Features.Companies.Dtos;

namespace Reservation.Application.Features.Companies.Commands
{
    public record RegisterCompanyCommand(RegisterCompanyDto model):IRequest<Shared.BaseResponse.ResponseDto<Guid>>;
}
