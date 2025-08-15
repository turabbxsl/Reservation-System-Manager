using MediatR;
using Microsoft.AspNetCore.Identity;
using Reservation.Application.Features.Customers.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Shared.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Application.Features.Customers.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UpdateCustomerCommandHandler(IUnitofWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var model = request.dto;

            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(request.customerId);
                if (customer == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Müştəri tapılmadı" }, 200);

                if (model.FullName.IsEdit)
                    customer.FullName = model.FullName.Value;

                if (model.Email.IsEdit)
                    customer.Email = model.Email.Value;

                if (model.PhoneNumber.IsEdit)
                    customer.PhoneNumber = model.PhoneNumber.Value;

                if (model.Note.IsEdit)
                    customer.Note = model.Note.Value;

                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { $"Müştəri məlumatları yenilənmədi" }, 200);
            }
        }
    }
}
