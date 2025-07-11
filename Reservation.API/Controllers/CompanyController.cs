using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Companies.Commands;
using Reservation.Application.Features.Companies.Dtos;
using Reservation.Shared.BaseResponse;

namespace Reservation.API.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCompanyDto dto)
        {
            var command = new RegisterCompanyCommand(dto);
            var companyId = await _mediator.Send(command);

            var response = ResponseDto<Guid>.SuccessResponse(companyId, "Şirkət uğurla yaradıldı", 201);

            return CreateActionResultInstance(response);
        }

    }
}
