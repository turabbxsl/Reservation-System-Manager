using MediatR;
using Reservation.Application.Features.Reservations.Commands;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
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

                var existCustReserv = await _unitofWork.Reservations.FindAsync(
                    x => x.CustomerId == dto.CustomerId && x.SpecialtyId == dto.SelectedSpecId && x.CompanyId == dto.CompanyId/* && x.ReservationTime==reservationDateTime*/
                    );
                if (existCustReserv.ToList()?.Count > 0)
                    return ResponseDto<Guid>.ErrorResponse(new List<string>() { $"Eyni şirkətə eyni xidmət gündə yalnız bir rezervasiya edə bilərsiniz" },200);

                var reservation = new Reservation.Domain.Entities.Reservation()
                {
                    Id = Guid.NewGuid(),
                    CompanyId = dto.CompanyId,
                    SpecialtyId = dto.SelectedSpecId,
                    CustomerId = dto.CustomerId,
                    ReservationTime = reservationDateTime,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitofWork.Reservations.AddAsync(reservation);

                foreach (var service in dto.SelectedServices)
                {
                    var reservationService = new ReservationSpecService()
                    {
                        Id = Guid.NewGuid(),
                        SpecialtyId = dto.SelectedSpecId,
                        ReservationId = reservation.Id,
                        ServiceId = service
                    };

                    await _unitofWork.ReservationSpecService.AddAsync(reservationService);
                }


                await _unitofWork.SaveChangesAsync();

                return ResponseDto<Guid>.SuccessResponse(reservation.Id, "Reservasiya yaradıldı", 201);
            }
            catch (Exception ex)
            {
                return ResponseDto<Guid>.ErrorResponse(new List<string>() { ex.InnerException?.Message }, "Gözlənilməz xəta", 200);
            }

        }
    }
}
