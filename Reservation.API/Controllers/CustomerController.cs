using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Customers.Commands;
using Reservation.Application.Features.Customers.Dtos;
using Reservation.Application.Features.Customers.Queries;
using Reservation.Application.Features.Reservations.Commands;
using Reservation.Application.Features.Reservations.Dtos;

namespace Reservation.API.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("details/{customerId:guid}")]
        public async Task<IActionResult> CustomerDetails(Guid customerId)
        {
            var query = new GetCustomerDetailsQuery(customerId);
            var result = await _mediator.Send(query);

            return CreateActionResultInstance(result);
        }

        [HttpGet("reservations/{customerId:guid}")]
        public async Task<IActionResult> CustomerReservation(Guid customerId)
        {
            var query = new GetCustomerReservationsQuery(customerId);
            var result = await _mediator.Send(query);

            return CreateActionResultInstance(result);
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerDto dto)
        {
            var command = new RegisterCustomerCommand(dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }

        [HttpPut("edit/{customerId:guid}")]
        public async Task<IActionResult> Edit(Guid customerId, [FromBody] UpdateCustomerCommandDto dto)
        {
            var command = new UpdateCustomerCommand(customerId, dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }

        [HttpPut("update-password/{customerId:guid}")]
        public async Task<IActionResult> UpdatePassword(Guid customerId, [FromBody] UpdatePasswordCommandDto dto)
        {
            var command = new UpdatePasswordCommand(customerId, dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }


        [HttpPut("cancel-reservation")]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangeStatusCommandDto dto)
        {
            var command = new ChangeStatusCommand(new Application.Features.Reservations.Dtos.ChangeStatusCommandDto(dto.reservationId, dto.status));
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }



    }
}
