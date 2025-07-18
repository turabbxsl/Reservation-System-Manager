using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Reservations.Commands;
using Reservation.Application.Features.Reservations.Dtos;
using Reservation.Application.Features.Reservations.Queries;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {

        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            var command = new CreateReservationCommand(dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }

        [HttpGet("available-times")]
        public async Task<IActionResult> GetAvailableTimes([FromQuery] Guid companyId, [FromQuery] Guid serviceId, [FromQuery] string date)
        {
            var result = await _mediator.Send(new GetAvailableReservationTimesQuery
            {
                CompanyId = companyId,
                ServiceId = serviceId,
                Date = DateTime.Parse(date)
            });

            return Ok(result);
        }


    }
}
