using MediatR;
using Reservation.Application.Features.Account.Commands;
using Reservation.Application.Features.Auth.ViewModels;
using Reservation.Application.Interfaces.Services;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Auth.Handlers
{
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, ResponseDto<LoginVM>>
    {
        private readonly IAuthService _authService;

        public LoginAccountCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseDto<LoginVM>> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var dto = request.model;
            return await _authService.AuthenticateAsync(dto.Email, dto.Password);
        }
    }
}
