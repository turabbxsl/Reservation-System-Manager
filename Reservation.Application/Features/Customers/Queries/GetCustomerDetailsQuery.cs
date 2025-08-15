using MediatR;
using Reservation.Application.Features.Customers.ViewModels;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Queries
{
    public record GetCustomerDetailsQuery(Guid customerId) : IRequest<ResponseDto<CustomerVM>>;
}
