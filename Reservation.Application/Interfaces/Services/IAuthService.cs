using Reservation.Application.Features.Auth.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginVM?>> AuthenticateAsync(string email, string password);
    }
}
