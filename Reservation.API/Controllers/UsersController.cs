using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Features.Users.Commands;
using Reservation.Application.Features.Users.Dtos;
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

        [HttpGet("{companyId:guid}/users")]
        public async Task<IActionResult> GetUsersByCompanyId(Guid companyId)
        {
            var users = await _mediator.Send(new GetUsersByCompanyIdQuery(companyId));
            return CreateActionResultInstance(users);
        }


        [HttpPost("users/{userId:guid}/{specialtyId:guid}/services/add")]
        public async Task<IActionResult> AddServices(Guid userId,Guid specialtyId, [FromBody] List<Guid> serviceIds)
        {
            var result = await _mediator.Send(new AddServicesStaffMemberCommand()
            {
                UserId = userId,
                SpecialtyId = specialtyId,
                ServiceIds = serviceIds
            });

            return CreateActionResultInstance(result);
        }

        [HttpPost("users/{userId:guid}/specialties/add")]
        public async Task<IActionResult> AddSpecialities(Guid userId, [FromBody] List<Guid> specialtyIds)
        {
            var result = await _mediator.Send(new AddSpecialitiesStaffMemberCommand(userId,specialtyIds));

            return CreateActionResultInstance(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStaffMemberCommand command)
        {
            var staffMemberId = await _mediator.Send(command);
            return CreateActionResultInstance(staffMemberId);
        }

        [HttpPut("{companyId:guid}/{id}")]
        public async Task<IActionResult> UpdateStaffMember(Guid companyId, Guid id, [FromBody] UpdateStaffMemberDto dto)
        {
            var result = await _mediator.Send(new UpdateStaffMemberCommand
            {
                CompanyId = companyId,
                NewStaffMemberData = dto
            });
            return CreateActionResultInstance(result);
        }


        [HttpDelete("staffmembers/{companyId:guid}/{userId:guid}")]
        public async Task<IActionResult> DeleteStaffMember(Guid companyId, Guid userId)
        {
            var result = await _mediator.Send(new DeleteStaffMemberCommand()
            {
                CompanyId = companyId,
                UserId = userId
            });
            return CreateActionResultInstance(result);
        }

        [HttpDelete("staffmembers/{userId:guid}/services/{serviceId:guid}")]
        public async Task<IActionResult> DeleteServiceStaffMember(Guid userId, Guid serviceId)
        {
            var result = await _mediator.Send(new DeleteServiceByStaffMemberCommand(userId,serviceId));
            return CreateActionResultInstance(result);
        }

        [HttpDelete("staffmembers/{userId:guid}/specialties/{specialtyId:guid}")]
        public async Task<IActionResult> DeleteSpeciatyStaffMember(Guid userId, Guid specialtyId)
        {
            var result = await _mediator.Send(new DeleteSpecialtyByStaffMemberCommand(userId, specialtyId));
            return CreateActionResultInstance(result);
        }



    }
}
