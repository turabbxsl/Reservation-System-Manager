using MediatR;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Customers.Commands;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Services;
using Reservation.Domain.Entities;
using Reservation.Infrastructure.Extensions;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Handlers
{
    public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, ResponseDto<Guid>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordService _passwordService;

        public RegisterCustomerHandler(IUnitofWork unitofWork, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IPasswordService passwordService)
        {
            _unitofWork = unitofWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _passwordService = passwordService;
        }

        public async Task<ResponseDto<Guid>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.model;

            var existingCustomerEmail = await _unitofWork.Customers.GetByEmailAsync(dto.Email);
            if (existingCustomerEmail is not null)
            {
                return ResponseDto<Guid>.ErrorResponse("Bu email ilə artıq qeydiyyat var", 200);
            }

            var existingCustomerPhone = await _unitofWork.Customers.GetByPhoneNumberAsync(dto.PhoneNumber);
            if (existingCustomerPhone is not null)
            {
                return ResponseDto<Guid>.ErrorResponse("Bu nömrə ilə artıq qeydiyyat var", 200);
            }

            var hashedPassword = _passwordService.Encrypt(dto.Password);

            var customer = new Customer
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PasswordHash = hashedPassword
            };

            await _unitofWork.Customers.AddAsync(customer);
            await _unitofWork.SaveChangesAsync();

            return ResponseDto<Guid>.SuccessResponse(customer.Id, "Müştəri uğurla qeydiyyatdan keçdi", 201);
        }
    }
}
