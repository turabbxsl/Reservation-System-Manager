using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Features.Services.Dtos;
using Reservation.Application.Features.Services.Queries;
using Reservation.Application.Features.Specialty.Commands;
using Reservation.Application.Features.Specialty.Queries;
using Reservation.Domain.Enums;

namespace Reservation.API.Controllers
{
    public class SpecialtiesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SpecialtiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// CompanyType-a görə Specialties gətir
        /// </summary>
        [HttpGet("by-company-type/{companyTypeId}")]
        public async Task<IActionResult> GetByCompanyType(CompanyType companyTypeId)
        {
            var result = await _mediator.Send(new GetSpecialtiesByCompanyTypeQuery
            {
                CompanyTypeId = companyTypeId
            });

            return CreateActionResultInstance(result);
        }

        /// <summary>
        /// Specialty-yə görə Services gətir
        /// </summary>
        [HttpGet("by-specialty/{companyId}/{specialtyId}")]
        public async Task<IActionResult> GetBySpecialty(Guid companyId, Guid specialtyId)
        {
            var result = await _mediator.Send(new GetServicesBySpecialtyQuery
            {
                SpecialtyId = specialtyId,
                CompanyId = companyId
            });

            return CreateActionResultInstance(result);
        }


        /// <summary>
        /// Details Specialty-yə görə Services gətir
        /// </summary>
        [HttpGet("details-by-specialty/{companyId}/{specialtyId}")]
        public async Task<IActionResult> GetDetailsBySpecialty(Guid companyId, Guid specialtyId)
        {
            var result = await _mediator.Send(new GetDetailsBySpecialtyQuery
            {
                SpecialtyId = specialtyId,
                CompanyId = companyId
            });

            return CreateActionResultInstance(result);
        }


        /// <summary>
        /// Bütün Specialtylər
        /// </summary>
        [HttpGet("{companyId:guid}")]
        public async Task<IActionResult> GetSpecialtiesByCompanyId(Guid companyId)
        {
            var result = await _mediator.Send(new GetSpecialtiesByCompanyIdQuery(companyId));
            return CreateActionResultInstance(result);
        }


        /// <summary>
        /// Xidmet kateqoriyanin yenilenmesi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update-specialty")]
        public async Task<IActionResult> UpdateSpecialty([FromBody] UpdateSpecialityDto model)
        {
            var result = await _mediator.Send(new UpdateSpecialityCommand(model.id, model.newSpecialityName));
            return CreateActionResultInstance(result);
        }

        /// <summary>
        /// Xidmetin yenilenmesi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update-inspecialty-service")]
        public async Task<IActionResult> UpdateInSpecialtyWithService([FromBody] UpdateInSpecialtyServiceDto model)
        {
            var result = await _mediator.Send(new UpdateInSpecialtyServiceCommand(model.serviceId, model.serviceName, model.price, model.duration));
            return CreateActionResultInstance(result);
        }


        /// <summary>
        /// Specialty silinmesi
        /// </summary>
        [HttpDelete("{companyId}/{companySpecialtyId}")]
        public async Task<IActionResult> DeleteSpecialty(Guid companyId, Guid companySpecialtyId)
        {
            var result = await _mediator.Send(new DeleteSpecialtyCommand(companyId, companySpecialtyId));
            return StatusCode(result.StatusCode, result);
        }


        /// <summary>
        /// Yeni Specialty
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create-with-services")]
        public async Task<IActionResult> AddSpecialtyWithServices([FromBody] AddSpecialtyWithServicesDto dto)
        {
            var command = _mapper.Map<AddSpecialtyWithServicesCommand>(dto);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }


        /// <summary>
        /// Specialty uygun servisin silinmesi
        /// </summary>
        [HttpDelete("delete-service/{companyId}/{serviceId}")]
        public async Task<IActionResult> DeleteServiceForSpecialty(Guid companyId, Guid serviceId)
        {
            var result = await _mediator.Send(new DeleteServiceCommand(companyId, serviceId));
            return StatusCode(result.StatusCode, result);
        }



        /// <summary>
        /// Mövcud xidmət kateqoriyasına uygun servisin silinmesi
        /// /// <param name="dto"></param>
        /// </summary>
        [HttpPost("inspeciality-create-services")]
        public async Task<IActionResult> AddInSpecialtyWithServices([FromBody] AddServicesInSpecialtyDto dto)
        {
            var command = _mapper.Map<AddServicesInSpecialtyCommand>(dto);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }
    }
}
