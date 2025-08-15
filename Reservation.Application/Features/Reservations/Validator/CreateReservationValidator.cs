using FluentValidation;
using Reservation.Application.Features.Reservations.Commands;

namespace Reservation.Application.Features.Reservations.Validator
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.model.CompanyId).NotEmpty().WithMessage("Şirkət boş buraxıla bilməz");
            RuleFor(x => x.model.SelectedSpecId).NotEmpty().WithMessage("Xidmət kateqoriyası boş buraxıla bilməz");
            RuleFor(x => x.model.CustomerId).NotEmpty().WithMessage("Müştəri boş buraxıla bilməz");
            RuleFor(x => x.model.ReservationDate).NotEmpty()/*.GreaterThanOrEqualTo(DateTime.Today)*/;
            RuleFor(x => x.model.ReservationTime).NotEmpty();
        }
    }
}
