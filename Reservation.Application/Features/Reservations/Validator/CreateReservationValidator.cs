using FluentValidation;
using Reservation.Application.Features.Reservations.Commands;

namespace Reservation.Application.Features.Reservations.Validator
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.model.CompanyId).NotEmpty();
            RuleFor(x => x.model.ServiceId).NotEmpty();
            RuleFor(x => x.model.CustomerId).NotEmpty();
            RuleFor(x => x.model.ReservationDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.model.ReservationTime).NotEmpty();
        }
    }
}
