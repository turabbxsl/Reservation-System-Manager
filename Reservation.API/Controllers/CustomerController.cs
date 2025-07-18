using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Customers.Commands;
using Reservation.Application.Features.Customers.Dtos;

namespace Reservation.API.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerDto dto)
        {
            var command = new RegisterCustomerCommand(dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }

    }
}
