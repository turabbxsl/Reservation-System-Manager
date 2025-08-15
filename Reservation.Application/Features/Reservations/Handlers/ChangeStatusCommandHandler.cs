using MediatR;
using Reservation.Application.Features.Reservations.Commands;
using Reservation.Application.Interfaces;
using Reservation.Shared.BaseResponse;

namespace Reservation.Application.Features.Reservations.Handlers
{
    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, ResponseDto<bool>>
    {
        private readonly IUnitofWork _unitOfWork;

        public ChangeStatusCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<bool>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = request.model;

                var reservation = await _unitOfWork.Reservations.GetByIdAsync(model.reservationId);
                if (reservation == null)
                    return ResponseDto<bool>.ErrorResponse(new List<string>() { "Rezervasiya tapılmadı" }, 200);

                reservation.Status = model.status;
                reservation.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Reservations.Update(reservation);
                await _unitOfWork.SaveChangesAsync();

                return ResponseDto<bool>.SuccessResponse(true, 200);
            }
            catch (Exception)
            {
                return ResponseDto<bool>.ErrorResponse(new List<string>() { "Status yenilənmədi" }, 200);
            }
        }
    }
}
