using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Companies.Commands;
using Reservation.Application.Features.Companies.Dtos;
using Reservation.Application.Features.Companies.Query;
using Reservation.Application.Features.Services.Queries;

namespace Reservation.API.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(companies);
        }

        [HttpGet("{companyId}/services")]
        public async Task<IActionResult> GetServicesByCompanyId(Guid companyId)
        {
            var services = await _mediator.Send(new GetServicesByCompanyQuery(companyId));
            return Ok(services);
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCompanyDto dto)
        {
            var command = new RegisterCompanyCommand(dto);
            var result = await _mediator.Send(command);

            return CreateActionResultInstance(result);
        }



    }
}
