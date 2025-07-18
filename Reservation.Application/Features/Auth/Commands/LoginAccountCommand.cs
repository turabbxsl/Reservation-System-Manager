using MediatR;
using Reservation.Application.Features.Account.Dtos;
using Reservation.Application.Features.Auth.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Account.Commands
{
    public record LoginAccountCommand(LoginDto model) : IRequest<ResponseDto<LoginVM>> { }
}
