using MediatR;
using Reservation.Application.Features.Customers.Commands;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Services;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Handlers
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IPasswordService _passwordService;

        public UpdatePasswordCommandHandler(IUnitofWork unitofWork, IPasswordService passwordService)
        {
            _unitofWork = unitofWork;
            _passwordService = passwordService;
        }

        public async Task<ResponseDto<bool>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = request.model;

                var customer = await _unitofWork.Customers.GetByIdAsync(request.customerId);
                if (customer == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Müştəri tapılmadı" }, 200);

                var currentPassword = _passwordService.Decrypt(customer.PasswordHash);
                var newPassword = _passwordService.Encrypt(model.newPassword);

                if (currentPassword != model.currentPassword)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Cari şifrəniz yanlışdır" }, 200);
                else if(currentPassword == newPassword)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Cari şifrəniz yeni şifrəniz ilə eyni ola bilməz" }, 200);

                customer.PasswordHash = newPassword;
                customer.UpdatedAt = DateTime.UtcNow;

                _unitofWork.Customers.Update(customer);
                await _unitofWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { "Şifrə yenilənmədi" }, 200);
            }

        }
    }
}
