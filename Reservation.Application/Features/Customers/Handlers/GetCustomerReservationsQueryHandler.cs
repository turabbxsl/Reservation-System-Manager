using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Features.Customers.Queries;
using Reservation.Application.Features.Customers.ViewModels;
using Reservation.Application.Features.Services.ViewModels;
using Reservation.Application.Interfaces;
using Reservation.Infrastructure.Extensions;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Customers.Handlers
{
    public class GetCustomerReservationsQueryHandler : IRequestHandler<GetCustomerReservationsQuery, ResponseDto<List<CustomerReservationVM>>>
    {
        private readonly IUnitofWork _unitOfWork;

        public GetCustomerReservationsQueryHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<List<CustomerReservationVM>>> Handle(GetCustomerReservationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reservations = await _unitOfWork.Reservations.FindWithIncludeAsync(
                    x => x.CustomerId == request.customerId,
                    query => query
                   .Include(b => b.Specialty)
                   .Include(c => c.Company)
                   .Include(p => p.ReservationSpecServices)
                        .ThenInclude(rss => rss.Service)
                    );

                var result = reservations.Select(x => new CustomerReservationVM()
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedAt,
                    ReservationDateTime = x.ReservationTime,
                    UpdatedAt = x.UpdatedAt,
                    Services = x.ReservationSpecServices.Where(s => s.ReservationId == x.Id && s.SpecialtyId == x.SpecialtyId)
                                                    .Select(s => new ServiceVM
                                                    {
                                                        Id = s.Service.Id,
                                                        Name = s.Service.Name,
                                                        Price = s.Service.Price,
                                                        Duration=s.Service.DurationInMinutes
                                                    }).ToList(),
                    Status = new() { Id = x.Status, StatusName = HelperExtensions.StatusName(x.Status) },

                    Company = new () { Id = x.Company.Id, Name = x.Company.Name },
                    Specialty = new() { Id = x.Specialty.Id, SpecialtyName = x.Specialty.Name },
                    ReservationNumber = x.ReservationNumer,
                    TotalPrice = x.ReservationSpecServices.Where(s => s.ReservationId == x.Id && s.SpecialtyId == x.SpecialtyId)
                                    .Sum(s => s.Service.Price)
                }).ToList();

                var p = result.Select(x => x.Company).FirstOrDefault();

                return ResponseDto<List<CustomerReservationVM>>.SuccessResponse(result, 200);
            }
            catch (Exception)
            {
                return ResponseDto<List<CustomerReservationVM>>.ErrorResponse("Rezervasiyalarınız gətirilə bilmədi", 200);
            }
        }
    }
}
