using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Account.Commands;
using Reservation.Application.Features.Account.Dtos;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var command = new LoginAccountCommand(dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }

    }
}
