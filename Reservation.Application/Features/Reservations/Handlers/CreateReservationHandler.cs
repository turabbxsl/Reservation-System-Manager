using MediatR;
using Reservation.Application.Features.Reservations.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Handlers
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, ResponseDto<Guid>>
    {

        private readonly IUnitofWork _unitofWork;

        public CreateReservationHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        async Task<ResponseDto<Guid>> IRequestHandler<CreateReservationCommand, ResponseDto<Guid>>.Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var dto = request.model;
            var reservationDateTime = dto.ReservationDate.Date + dto.ReservationTime;

            var reservation = new Reservation.Domain.Entities.Reservation()
            {
                Id = Guid.NewGuid(),
                CompanyId = dto.CompanyId,
                ServiceId = dto.ServiceId,
                CustomerId = dto.CustomerId,
                ReservationTime = reservationDateTime,
                CreatedAt = DateTime.UtcNow
            };

            await _unitofWork.Reservations.AddAsync(reservation);
            await _unitofWork.SaveChangesAsync();

            return ResponseDto<Guid>.SuccessResponse(reservation.Id, "Reservasiya yaradıldı", 201);
        }
    }
}
