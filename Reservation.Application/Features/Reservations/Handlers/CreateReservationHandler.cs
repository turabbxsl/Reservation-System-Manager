using MediatR;
using Reservation.Application.Features.Reservations.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;
using System.Globalization;

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

            try
            {
                if (!DateTime.TryParseExact(dto.ReservationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                    return ResponseDto<Guid>.ErrorResponse("Yanlış tarix formatı", 200);

                if (!TimeSpan.TryParseExact(dto.ReservationTime, "hh\\:mm", CultureInfo.InvariantCulture, out var time))
                    return ResponseDto<Guid>.ErrorResponse("Yanlış saat formatı", 200);

                var reservationDateTime = date.Date + time;

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
            catch (Exception ex)
            {
                return ResponseDto<Guid>.ErrorResponse(new List<string>() {ex.InnerException?.Message },"Gözlənilməz xəta", 200);

            }

        }
    }
}
