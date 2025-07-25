using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Users.Queries;

namespace Reservation.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId}/users")]
        public async Task<IActionResult> GetUsersByCompanyId(Guid companyId)
        {
            var users = await _mediator.Send(new GetUsersByCompanyIdQuery(companyId));
            return CreateActionResultInstance(users);
        }


    }
}
