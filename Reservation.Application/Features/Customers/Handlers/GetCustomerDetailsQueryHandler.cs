using MediatR;
using Reservation.Application.Features.Customers.Queries;
using Reservation.Application.Features.Customers.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Handlers
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, ResponseDto<CustomerVM>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetCustomerDetailsQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ResponseDto<CustomerVM>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitofWork.Customers.GetByIdAsync(request.customerId);
                if (customer is null)
                    return ResponseDto<CustomerVM>.ErrorResponse(new List<string>() { $"Müştəri tapılmadı" }, 200);

                var result = new CustomerVM()
                {
                    FullName = customer.FullName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    ClientCode = customer.ClientCode,
                    Note = customer.Note,
                    IsEmail = customer.IsEmail,
                    IsSms = customer.IsSms
                };

                return ResponseDto<CustomerVM>.SuccessResponse(result, 200);
            }
            catch (Exception)
            {
                return ResponseDto<CustomerVM>.ErrorResponse(new List<string>() { $"Müştəri məlumatları gətirilə bilmədi" }, 200);
            }
        }
    }
}
